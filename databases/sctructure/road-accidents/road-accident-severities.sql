CREATE TABLE road_accidents.road_accident_severities (
	road_accident_id uuid NOT NULL,
    severity road_accidents.accident_severity,
    CONSTRAINT pk_road_accidents_road_accidents_severities_road_accident_id PRIMARY KEY (road_accident_id),
    CONSTRAINT fk_road_accidents_road_accidents_severities_road_accidents FOREIGN KEY (road_accident_id) REFERENCES road_accidents.road_accidents (id)
);
