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
  "Id": 0,
  "Name": "Test animal",
  "LegCount": 17
}'
}

insert_many() {
curl -X 'POST' \
  "${api_base_url}/animals/insert/many" \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '[
  {"Id": 0,"Name": "Sandy the snake","LegCount": 0},
  {"Id": 0,"Name": "Bob the beaver","LegCount": 2},
  {"Id": 0,"Name": "Cooper the cow","LegCount": 4},
  {"Id": 0,"Name": "Cocoa the cassowary","LegCount": 2},
  {"Id": 0,"Name": "Dina the diglet","LegCount": 0}
  ]'
}
