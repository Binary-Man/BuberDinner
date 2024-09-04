# Buber Dinner API

- [Buber Dinner API](#buber-dinner-api)
  - [Create Dinner](#create-dinner)
    - [Create Dinner Request](#create-dinner-request)
    - [Create Dinner Response](#create-dinner-response)
  - [Get Dinner](#get-dinner)
    - [Get Dinner Request](#get-dinner-request)
    - [Get Dinner Response](#get-dinner-response)
  - [Update Dinner](#update-dinner)
    - [Update Dinner Request](#update-dinner-request)
    - [Update Dinner Response](#update-dinner-response)
  - [Delete Dinner](#delete-dinner)
    - [Delete Dinner Request](#delete-dinner-request)
    - [Delete Dinner Response](#delete-dinner-response)

## Auth 

## Register

```json
{
  "firstName": "Web",
  "lastName": "Developer",
  "email": "web_developer@hotmail.com",
  "password": "2022-04-08T11:00:00" 
}
```


## Create Dinner

### Create Dinner Request

```js
POST / dinners;
```

```json
{
  "name": "Vegan Sunshine",
  "description": "Vegan everything! Join us for a healthy dinner..",
  "startDateTime": "2022-04-08T08:00:00",
  "endDateTime": "2022-04-08T11:00:00",
  "savory": ["Oatmeal", "Avocado Toast", "Omelette", "Salad"],
  "Sweet": ["Cookie"]
}
```

### Create Dinner Response

```js
201 Created
```

```yml
Location: {{host}}/Dinners/{{id}}
```

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "name": "Vegan Sunshine",
  "description": "Vegan everything! Join us for a healthy dinner..",
  "startDateTime": "2022-04-08T08:00:00",
  "endDateTime": "2022-04-08T11:00:00",
  "lastModifiedDateTime": "2022-04-06T12:00:00",
  "savory": ["Oatmeal", "Avocado Toast", "Omelette", "Salad"],
  "Sweet": ["Cookie"]
}
```

## Get Dinner

### Get Dinner Request

```js
GET /dinners/{{id}}
```

### Get Dinner Response

```js
200 Ok
```

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "name": "Vegan Sunshine",
  "description": "Vegan everything! Join us for a healthy dinner..",
  "startDateTime": "2022-04-08T08:00:00",
  "endDateTime": "2022-04-08T11:00:00",
  "lastModifiedDateTime": "2022-04-06T12:00:00",
  "savory": ["Oatmeal", "Avocado Toast", "Omelette", "Salad"],
  "Sweet": ["Cookie"]
}
```

## Update Dinner

### Update Dinner Request

```js
PUT /dinners/{{id}}
```

```json
{
  "name": "Vegan Sunshine",
  "description": "Vegan everything! Join us for a healthy dinner..",
  "startDateTime": "2022-04-08T08:00:00",
  "endDateTime": "2022-04-08T11:00:00",
  "savory": ["Oatmeal", "Avocado Toast", "Omelette", "Salad"],
  "Sweet": ["Cookie"]
}
```

### Update Dinner Response

```js
204 No Content
```

or

```js
201 Created
```

```yml
Location: {{host}}/Dinners/{{id}}
```

## Delete Dinner

### Delete Dinner Request

```js
DELETE /dinners/{{id}}
```

### Delete Dinner Response

```js
204 No Content
```
