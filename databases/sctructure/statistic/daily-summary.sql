CREATE TABLE statistic.daily_summary(
    "date" timestamp with time zone NOT NULL,
    region_number integer NOT NULL,
    dtp_count integer NOT NULL,
    dead_count integer NOT NULL,
    injury_count integer NOT NULL,
    CONSTRAINT pk_statistic_daily_summary_date_region_number PRIMARY KEY ("date", region_number)
);
