run-dev:
	docker-compose -f docker-compose.yml -f docker-compose.dev.yml up --build -d

run-prod:
	docker-compose -f docker-compose.yml -f docker-compose.prod.yml up --build -d

logs:
	docker-compose logs