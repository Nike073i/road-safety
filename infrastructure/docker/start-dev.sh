dir=`dirname $0`
docker compose -f $dir/infrastructure.yml -f $dir/infrastructure.dev.yml -p road_safety up --build