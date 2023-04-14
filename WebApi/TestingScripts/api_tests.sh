#!/usr/bin/env bash

api_base_url="localhost:8080"

get_all() {
curl -X 'GET' \
  "${api_base_url}/animals/list/all" \
  -H 'accept: */*'
}

get_one() {
curl -X 'GET' \
  "${api_base_url}/animals/list/single/${1}" \
  -H 'accept: */*'
}

insert_one() {
curl -X 'POST' \
  "${api_base_url}/animals/insert/single" \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": 0,
  "name": "Test animal",
  "legCount": 17
}'
}
