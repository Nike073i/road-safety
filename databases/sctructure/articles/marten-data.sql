CREATE FUNCTION articles.mt_archive_stream(streamid uuid) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
  update articles.mt_streams set is_archived = TRUE where id = streamid ;
  update articles.mt_events set is_archived = TRUE where stream_id = streamid ;
END;
$$;

CREATE FUNCTION articles.mt_grams_array(words text) RETURNS text[]
    LANGUAGE plpgsql IMMUTABLE STRICT
    AS $$
        DECLARE
result text[];
        DECLARE
word text;
        DECLARE
clean_word text;
BEGIN
    FOREACH word IN ARRAY string_to_array(words, ' ')
    LOOP 
        clean_word = regexp_replace(word, '[^a-zA-Z0-9]+', '','g');
        FOR i IN 1 .. length(clean_word)
        LOOP
            result := result || quote_literal(substr(lower(clean_word), i, 1));
            result := result || quote_literal(substr(lower(clean_word), i, 2));
            result := result || quote_literal(substr(lower(clean_word), i, 3));
        END LOOP;
    END LOOP;

RETURN ARRAY(SELECT DISTINCT e FROM unnest(result) AS a(e) ORDER BY e);
END;
$$;

CREATE FUNCTION articles.mt_grams_query(text) RETURNS tsquery
    LANGUAGE plpgsql IMMUTABLE STRICT
    AS $_$
BEGIN
RETURN (SELECT array_to_string(articles.mt_grams_array($1), ' & ') ::tsquery);
END
$_$;

CREATE FUNCTION articles.mt_grams_vector(text) RETURNS tsvector
    LANGUAGE plpgsql IMMUTABLE STRICT
    AS $_$
BEGIN
RETURN (SELECT array_to_string(articles.mt_grams_array($1), ' ') ::tsvector);
END
$_$;

CREATE FUNCTION articles.mt_immutable_date(value text) RETURNS date
    LANGUAGE sql IMMUTABLE
    AS $$
select value::date

$$;

CREATE FUNCTION articles.mt_immutable_time(value text) RETURNS time without time zone
    LANGUAGE sql IMMUTABLE
    AS $$
select value::time

$$;

CREATE FUNCTION articles.mt_immutable_timestamp(value text) RETURNS timestamp without time zone
    LANGUAGE sql IMMUTABLE
    AS $$
select value::timestamp

$$;

CREATE FUNCTION articles.mt_immutable_timestamptz(value text) RETURNS timestamp with time zone
    LANGUAGE sql IMMUTABLE
    AS $$
select value::timestamptz

$$;

CREATE FUNCTION articles.mt_insert_deadletterevent(doc jsonb, docdotnettype character varying, docid uuid, docversion uuid) RETURNS uuid
    LANGUAGE plpgsql
    AS $$
BEGIN
INSERT INTO articles.mt_doc_deadletterevent ("data", "mt_dotnet_type", "id", "mt_version", mt_last_modified) VALUES (doc, docDotNetType, docId, docVersion, transaction_timestamp());

  RETURN docVersion;
END;
$$;

CREATE FUNCTION articles.mt_jsonb_append(jsonb, text[], jsonb, boolean) RETURNS jsonb
    LANGUAGE plpgsql
    AS $_$
DECLARE
    retval ALIAS FOR $1;
    location ALIAS FOR $2;
    val ALIAS FOR $3;
    if_not_exists ALIAS FOR $4;
    tmp_value jsonb;
BEGIN
    tmp_value = retval #> location;
    IF tmp_value IS NOT NULL AND jsonb_typeof(tmp_value) = 'array' THEN
        CASE
            WHEN NOT if_not_exists THEN
                retval = jsonb_set(retval, location, tmp_value || val, FALSE);
            WHEN jsonb_typeof(val) = 'object' AND NOT tmp_value @> jsonb_build_array(val) THEN
                retval = jsonb_set(retval, location, tmp_value || val, FALSE);
            WHEN jsonb_typeof(val) <> 'object' AND NOT tmp_value @> val THEN
                retval = jsonb_set(retval, location, tmp_value || val, FALSE);
            ELSE NULL;
            END CASE;
    END IF;
    RETURN retval;
END;
$_$;

CREATE FUNCTION articles.mt_jsonb_copy(jsonb, text[], text[]) RETURNS jsonb
    LANGUAGE plpgsql
    AS $_$
DECLARE
    retval ALIAS FOR $1;
    src_path ALIAS FOR $2;
    dst_path ALIAS FOR $3;
    tmp_value jsonb;
BEGIN
    tmp_value = retval #> src_path;
    retval = articles.mt_jsonb_fix_null_parent(retval, dst_path);
    RETURN jsonb_set(retval, dst_path, tmp_value::jsonb, TRUE);
END;
$_$;

CREATE FUNCTION articles.mt_jsonb_duplicate(jsonb, text[], jsonb) RETURNS jsonb
    LANGUAGE plpgsql
    AS $_$
DECLARE
    retval ALIAS FOR $1;
    location ALIAS FOR $2;
    targets ALIAS FOR $3;
    tmp_value jsonb;
    target_path text[];
    target text;
BEGIN
    FOR target IN SELECT jsonb_array_elements_text(targets)
    LOOP
        target_path = articles.mt_jsonb_path_to_array(target, '\.');
        retval = articles.mt_jsonb_copy(retval, location, target_path);
    END LOOP;

    RETURN retval;
END;
$_$;

CREATE FUNCTION articles.mt_jsonb_fix_null_parent(jsonb, text[]) RETURNS jsonb
    LANGUAGE plpgsql
    AS $_$
DECLARE
retval ALIAS FOR $1;
    dst_path ALIAS FOR $2;
    dst_path_segment text[] = ARRAY[]::text[];
    dst_path_array_length integer;
    i integer = 1;
BEGIN
    dst_path_array_length = array_length(dst_path, 1);
    WHILE i <=(dst_path_array_length - 1)
    LOOP
        dst_path_segment = dst_path_segment || ARRAY[dst_path[i]];
        IF retval #> dst_path_segment = 'null'::jsonb THEN
            retval = jsonb_set(retval, dst_path_segment, '{}'::jsonb, TRUE);
        END IF;
        i = i + 1;
    END LOOP;

    RETURN retval;
END;
$_$;

CREATE FUNCTION articles.mt_jsonb_increment(jsonb, text[], numeric) RETURNS jsonb
    LANGUAGE plpgsql
    AS $_$
DECLARE
retval ALIAS FOR $1;
    location ALIAS FOR $2;
    increment_value ALIAS FOR $3;
    tmp_value jsonb;
BEGIN
    tmp_value = retval #> location;
    IF tmp_value IS NULL THEN
        tmp_value = to_jsonb(0);
END IF;

RETURN jsonb_set(retval, location, to_jsonb(tmp_value::numeric + increment_value), TRUE);
END;
$_$;

CREATE FUNCTION articles.mt_jsonb_insert(jsonb, text[], jsonb, integer, boolean) RETURNS jsonb
    LANGUAGE plpgsql
    AS $_$
DECLARE
    retval ALIAS FOR $1;
    location ALIAS FOR $2;
    val ALIAS FOR $3;
    elm_index ALIAS FOR $4;
    if_not_exists ALIAS FOR $5;
    tmp_value jsonb;
BEGIN
    tmp_value = retval #> location;
    IF tmp_value IS NOT NULL AND jsonb_typeof(tmp_value) = 'array' THEN
        IF elm_index IS NULL THEN
            elm_index = jsonb_array_length(tmp_value) + 1;
        END IF;
        CASE
            WHEN NOT if_not_exists THEN
                retval = jsonb_insert(retval, location || elm_index::text, val);
            WHEN jsonb_typeof(val) = 'object' AND NOT tmp_value @> jsonb_build_array(val) THEN
                retval = jsonb_insert(retval, location || elm_index::text, val);
            WHEN jsonb_typeof(val) <> 'object' AND NOT tmp_value @> val THEN
                retval = jsonb_insert(retval, location || elm_index::text, val);
            ELSE NULL;
        END CASE;
    END IF;
    RETURN retval;
END;
$_$;

CREATE FUNCTION articles.mt_jsonb_move(jsonb, text[], text) RETURNS jsonb
    LANGUAGE plpgsql
    AS $_$
DECLARE
    retval ALIAS FOR $1;
    src_path ALIAS FOR $2;
    dst_name ALIAS FOR $3;
    dst_path text[];
    tmp_value jsonb;
BEGIN
    tmp_value = retval #> src_path;
    retval = retval #- src_path;
    dst_path = src_path;
    dst_path[array_length(dst_path, 1)] = dst_name;
    retval = articles.mt_jsonb_fix_null_parent(retval, dst_path);
    RETURN jsonb_set(retval, dst_path, tmp_value, TRUE);
END;
$_$;

CREATE FUNCTION articles.mt_jsonb_patch(jsonb, jsonb) RETURNS jsonb
    LANGUAGE plpgsql
    AS $_$
DECLARE
    retval ALIAS FOR $1;
    patchset ALIAS FOR $2;
    patch jsonb;
    patch_path text[];
    value jsonb;
BEGIN
    FOR patch IN SELECT * from jsonb_array_elements(patchset)
    LOOP
        patch_path = articles.mt_jsonb_path_to_array((patch->>'path')::text, '\.');

        CASE patch->>'type'
            WHEN 'set' THEN
                retval = jsonb_set(retval, patch_path,(patch->'value')::jsonb, TRUE);
        WHEN 'delete' THEN
                retval = retval#-patch_path;
        WHEN 'append' THEN
                retval = articles.mt_jsonb_append(retval, patch_path,(patch->'value')::jsonb, FALSE);
        WHEN 'append_if_not_exists' THEN
                retval = articles.mt_jsonb_append(retval, patch_path,(patch->'value')::jsonb, TRUE);
        WHEN 'insert' THEN
                retval = articles.mt_jsonb_insert(retval, patch_path,(patch->'value')::jsonb,(patch->>'index')::integer, FALSE);
        WHEN 'insert_if_not_exists' THEN
                retval = articles.mt_jsonb_insert(retval, patch_path,(patch->'value')::jsonb,(patch->>'index')::integer, TRUE);
        WHEN 'remove' THEN
                retval = articles.mt_jsonb_remove(retval, patch_path,(patch->'value')::jsonb);
        WHEN 'duplicate' THEN
                retval = articles.mt_jsonb_duplicate(retval, patch_path,(patch->'targets')::jsonb);
        WHEN 'rename' THEN
                retval = articles.mt_jsonb_move(retval, patch_path,(patch->>'to')::text);
        WHEN 'increment' THEN
                retval = articles.mt_jsonb_increment(retval, patch_path,(patch->>'increment')::numeric);
        WHEN 'increment_float' THEN
                retval = articles.mt_jsonb_increment(retval, patch_path,(patch->>'increment')::numeric);
        ELSE NULL;
        END CASE;
    END LOOP;
    RETURN retval;
END;
$_$;

CREATE FUNCTION articles.mt_jsonb_path_to_array(text, character) RETURNS text[]
    LANGUAGE plpgsql
    AS $_$
DECLARE
    location ALIAS FOR $1;
    regex_pattern ALIAS FOR $2;
BEGIN
RETURN regexp_split_to_array(location, regex_pattern)::text[];
END;
$_$;

CREATE FUNCTION articles.mt_jsonb_remove(jsonb, text[], jsonb) RETURNS jsonb
    LANGUAGE plpgsql
    AS $_$
DECLARE
    retval ALIAS FOR $1;
    location ALIAS FOR $2;
    val ALIAS FOR $3;
    tmp_value jsonb;
BEGIN
    tmp_value = retval #> location;
    IF tmp_value IS NOT NULL AND jsonb_typeof(tmp_value) = 'array' THEN
        tmp_value =(SELECT jsonb_agg(elem)
        FROM jsonb_array_elements(tmp_value) AS elem
        WHERE elem <> val);

        IF tmp_value IS NULL THEN
            tmp_value = '[]'::jsonb;
        END IF;
    END IF;
    RETURN jsonb_set(retval, location, tmp_value, FALSE);
END;
$_$;

CREATE FUNCTION articles.mt_mark_event_progression(name character varying, last_encountered bigint) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
INSERT INTO articles.mt_event_progression (name, last_seq_id, last_updated)
VALUES (name, last_encountered, transaction_timestamp())
ON CONFLICT ON CONSTRAINT pk_mt_event_progression
    DO
UPDATE SET last_seq_id = last_encountered, last_updated = transaction_timestamp();

END;

$$;

CREATE FUNCTION articles.mt_quick_append_events(stream uuid, stream_type character varying, tenantid character varying, event_ids uuid[], event_types character varying[], dotnet_types character varying[], bodies jsonb[]) RETURNS integer[]
    LANGUAGE plpgsql
    AS $$
DECLARE
	event_version int;
	event_type varchar;
	event_id uuid;
	body jsonb;
	index int;
	seq int;
    actual_tenant varchar;
	return_value int[];
BEGIN
	select version into event_version from articles.mt_streams where id = stream;
	if event_version IS NULL then
		event_version = 0;
		insert into articles.mt_streams (id, type, version, timestamp, tenant_id) values (stream, stream_type, 0, now(), tenantid);
    else
        if tenantid IS NOT NULL then
            select tenant_id into actual_tenant from articles.mt_streams where id = stream;
            if actual_tenant != tenantid then
                RAISE EXCEPTION 'The tenantid does not match the existing stream';
            end if;
        end if;
	end if;

	index := 1;
	return_value := ARRAY[event_version + array_length(event_ids, 1)];

	foreach event_id in ARRAY event_ids
	loop
	    seq := nextval('articles.mt_events_sequence');
		return_value := array_append(return_value, seq);

	    event_version := event_version + 1;
		event_type = event_types[index];
		body = bodies[index];

		insert into articles.mt_events
			(seq_id, id, stream_id, version, data, type, tenant_id, timestamp, mt_dotnet_type, is_archived)
		values
			(seq, event_id, stream, event_version, body, event_type, tenantid, (now() at time zone 'utc'), dotnet_types[index], FALSE);

		index := index + 1;
	end loop;

	update articles.mt_streams set version = event_version, timestamp = now() where id = stream;

	return return_value;
END
$$;

CREATE FUNCTION articles.mt_update_deadletterevent(doc jsonb, docdotnettype character varying, docid uuid, docversion uuid) RETURNS uuid
    LANGUAGE plpgsql
    AS $$
DECLARE
  final_version uuid;
BEGIN
  UPDATE articles.mt_doc_deadletterevent SET "data" = doc, "mt_dotnet_type" = docDotNetType, "mt_version" = docVersion, mt_last_modified = transaction_timestamp() where id = docId;

  SELECT mt_version FROM articles.mt_doc_deadletterevent into final_version WHERE id = docId ;
  RETURN final_version;
END;
$$;

CREATE FUNCTION articles.mt_upsert_deadletterevent(doc jsonb, docdotnettype character varying, docid uuid, docversion uuid) RETURNS uuid
    LANGUAGE plpgsql
    AS $$
DECLARE
  final_version uuid;
BEGIN
INSERT INTO articles.mt_doc_deadletterevent ("data", "mt_dotnet_type", "id", "mt_version", mt_last_modified) VALUES (doc, docDotNetType, docId, docVersion, transaction_timestamp())
  ON CONFLICT (id)
  DO UPDATE SET "data" = doc, "mt_dotnet_type" = docDotNetType, "mt_version" = docVersion, mt_last_modified = transaction_timestamp();

  SELECT mt_version FROM articles.mt_doc_deadletterevent into final_version WHERE id = docId ;
  RETURN final_version;
END;
$$;

SET default_tablespace = '';

SET default_table_access_method = heap;

CREATE TABLE articles.mt_doc_deadletterevent (
    id uuid NOT NULL,
    data jsonb NOT NULL,
    mt_last_modified timestamp with time zone DEFAULT transaction_timestamp(),
    mt_version uuid DEFAULT (md5(((random())::text || (clock_timestamp())::text)))::uuid NOT NULL,
    mt_dotnet_type character varying
);

CREATE TABLE articles.mt_event_progression (
    name character varying NOT NULL,
    last_seq_id bigint,
    last_updated timestamp with time zone DEFAULT transaction_timestamp()
);

CREATE TABLE articles.mt_events (
    seq_id bigint NOT NULL,
    id uuid NOT NULL,
    stream_id uuid,
    version bigint NOT NULL,
    data jsonb NOT NULL,
    type character varying(500) NOT NULL,
    "timestamp" timestamp with time zone DEFAULT '2025-01-28 20:45:05.968933+00'::timestamp with time zone NOT NULL,
    tenant_id character varying DEFAULT '*DEFAULT*'::character varying,
    mt_dotnet_type character varying,
    is_archived boolean DEFAULT false
);

CREATE SEQUENCE articles.mt_events_sequence
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE articles.mt_events_sequence OWNED BY articles.mt_events.seq_id;

CREATE TABLE articles.mt_streams (
    id uuid NOT NULL,
    type character varying,
    version bigint,
    "timestamp" timestamp with time zone DEFAULT now() NOT NULL,
    snapshot jsonb,
    snapshot_version integer,
    created timestamp with time zone DEFAULT now() NOT NULL,
    tenant_id character varying DEFAULT '*DEFAULT*'::character varying,
    is_archived boolean DEFAULT false
);

ALTER TABLE ONLY articles.mt_event_progression
    ADD CONSTRAINT pk_mt_event_progression PRIMARY KEY (name);

ALTER TABLE ONLY articles.mt_doc_deadletterevent
    ADD CONSTRAINT pkey_mt_doc_deadletterevent_id PRIMARY KEY (id);

ALTER TABLE ONLY articles.mt_events
    ADD CONSTRAINT pkey_mt_events_seq_id PRIMARY KEY (seq_id);

ALTER TABLE ONLY articles.mt_streams
    ADD CONSTRAINT pkey_mt_streams_id PRIMARY KEY (id);

CREATE UNIQUE INDEX pk_mt_events_stream_and_version ON articles.mt_events USING btree (stream_id, version);

ALTER TABLE ONLY articles.mt_events
    ADD CONSTRAINT fkey_mt_events_stream_id FOREIGN KEY (stream_id) REFERENCES articles.mt_streams(id) ON DELETE CASCADE;
