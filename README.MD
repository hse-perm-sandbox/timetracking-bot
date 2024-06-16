# timetracking-bot 

Бот, разработанный для платформы Telegram, который помогает пользователям отслеживать время, проведенное за каким-либо занятием, и предоставляет статистику затраченного времени.

Запуск в docker-контейнере локально: 
`docker-compose -f docker-compose.local.yml --env-file .env.local up --build`

Запуск dev-версии в docker-контейнере на сервере
```commandline
docker pull ghcr.io/hse-perm-projects-23-24/timetracking-bot:dev;
docker volume create timetracking-data-dev;
docker run -v timetracking-data-dev:/data -e TIMETRACKING_TG_TOKEN=set_token_value --name timetracking-bot-dev ghcr.io/hse-perm-projects-23-24/timetracking-bot:dev;
```