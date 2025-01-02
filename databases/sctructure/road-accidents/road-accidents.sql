CREATE TABLE road_accidents.road_accidents(
    id uuid NOT NULL,
    gibdd_id integer NOT NULL,
    occured_on timestamp with time zone NOT NULL,
    location geography(POINT,4326) NOT NULL,
    address road_accidents.address NOT NULL,
    wind road_accidents.wind NOT NULL,
    visibility road_accidents.visibility NOT NULL,
    light road_accidents.light NOT NULL,
    category road_accidents.category NOT NULL,
    road_condition road_accidents.road_condition NOT NULL,
    dead_count integer NOT NULL,
    vehicles jsonb NOT NULL,
    participant jsonb NOT NULL,
    CONSTRAINT pk_road_accidents_road_accidents_id PRIMARY KEY (id)
);
