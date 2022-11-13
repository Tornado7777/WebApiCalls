«Необходимо разработать систему учета контактов и звонков»

Аутентификация происходит по 
phone: "+1-111-111-11-11" 
из таблицы contacts.
Отключена авторизация для работы с табицей contacts 

Параметры подключения к БД в appsettings.json

В миграции есть данные для тестирования

http://localhost:5000

- POST Authenticate/login

```json
{
  "phone": "+1-111-111-11-11" 
}

Валидация на вид +Х-ХХХ-ХХХ-ХХ-ХХ, где Х-число

Return:
```json
{
  "status": 0,
  "session": {
    "sessionId": 13,
    "sessionToken": "token",
    "contactId": 1
  }
}

-Get Authenticate/session

HEADER: ```Authorization: Bearer {token}```

Return:
```json
{
  "status": 0,
  "session": {
    "sessionId": 13,
    "sessionToken": "token",
    "contactId": 1
  }
}

[Call]
HEADER: ```Authorization: Bearer {token}```

- PUT Call/create

```json
{
  "toPhone": "+X-XXX-XXX-XX-XX",
  "timeStart": "2022-11-13T08:22:10.749Z",
  "timeEnd": "2022-11-13T08:22:10.749Z"
}

Валидация на вид +Х-ХХХ-ХХХ-ХХ-ХХ, где Х-число
Return:

```json
{
  "callId": 0,
  "fromPhone": "string",
  "toPhone": "string",
  "timeStart": "2022-11-13T08:23:25.852Z",
  "timeEnd": "2022-11-13T08:23:25.852Z"
}

- Get Call/all

Return:

```json
[
  {
    "callId": 0,
    "fromPhone": "string",
    "toPhone": "string",
    "timeStart": "2022-11-13T08:26:34.808Z",
    "timeEnd": "2022-11-13T08:26:34.808Z"
  },
   .....
]

- Get Call/get/{id}

Return:

```json
  {
    "callId": id,
    "fromPhone": "string",
    "toPhone": "string",
    "timeStart": "2022-11-13T08:26:34.808Z",
    "timeEnd": "2022-11-13T08:26:34.808Z"
  }

- Get Call/update

```json
{
    "callId": id,
    "fromPhone": "string",
    "toPhone": "string",
    "timeStart": "2022-11-13T08:26:34.808Z",
    "timeEnd": "2022-11-13T08:26:34.808Z"
  }

Return:

```json
  {
    "callId": id,
    "fromPhone": "string",
    "toPhone": "string",
    "timeStart": "2022-11-13T08:26:34.808Z",
    "timeEnd": "2022-11-13T08:26:34.808Z"
  }

- Delete Call/delete/{id}

[Contact]
HEADER: ```Authorization: Bearer {token}```

- PUT Contact/create

```json
{
  "phone": "string",
  "fio": "string",
  "company": "string",
  "description": "string"
}

Валидация на вид +Х-ХХХ-ХХХ-ХХ-ХХ, где Х-число
Return:

ContactId

- Get Contact/all

Return:

```json
[
  {
  "phone": "string",
  "fio": "string",
  "company": "string",
  "description": "string"
  },
   .....
]

- Get Contact/get/{id}

Return:

```json
  {
    "contactId": 0,
    "phone": "string",
    "fio": "string",
    "company": "string",
    "description": "string"
  }


- Get Contact/update

```json
{
    "contactId": 0,
    "phone": "string",
    "fio": "string",
    "company": "string",
    "description": "string"
  }

Return:

```json
  {
    "contactId": 0,
    "phone": "string",
    "fio": "string",
    "company": "string",
    "description": "string"
  }


- Delete Contact/delete/{id}

