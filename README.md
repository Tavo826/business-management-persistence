# business-management-persistence

### Implement Migrations and Update Database

**Create migration**

```bash
cd BusinessPersistance

dotnet ef migrations add InitialCreate --project ../Persistence --startup-project .
```

**Apply migration and update database**

```bash
cd BusinessPersistance

dotnet ef database update --project ../Persistence --startup-project .
```


### Endpoints

1. **Name:** Get API status  
**Description:** Get API status from controller  
**Endpoint:** /api/v1/Message/GetApiStatus  
**Type:** GET
- **Response:**
    - **Status code:** 200  
        **Body:**
        ```json
        {
            "success": true,
            "message": "Status OK!",
            "statusCode": 200,
            "data": null
        }
        ```


2. **Name:** Get DB status  
**Description:** Get status from database  
**Endpoint:** /api/v1/Message/GetDbStatus  
**Type:** GET
- **Response:**
    - **Status code:** 200  
        **Body:**
        ```json
        {
            "success": true,
            "message": "Status OK!",
            "statusCode": 200,
            "data": null
        }
        ```

3. **Name:** Get messages by id  
**Description:** Get all messages related to conversation id  
**Endpoint:** /Message/GetMessageById/**{{id}}**  
**Type:** GET
- **Request**
    - **Params:**
        - **Name:** id  
            **Type:** Path  
            **Example:** wamid.HBgMNTczMjE3NjM3NjYzFQIAEhggODk0MjExMkEyQ0YxNUVBMDlGNTRERDIwQzVFMTE4RUYA
- **Response:**
    - **Status code:** 200  
        **Body:**
        ```json
        {
            "success": true,
            "message": "Messages associated to wamid.HBgMNTczMjE3NjM3NjYzFQIAEhggODk0MjExMkEyQ0YxNUVBMDlGNTRERDIwQzVFMTE4RUYB successfully retrieved",
            "statusCode": 200,
            "data": [
                {
                    "id": "9128977c-dd41-4304-25c2-08ddd48d74ef",
                    "messageId": "wamid.HBgMNTczMjE3NjM3NjYzFQIAEhggODk0MjExMkEyQ0YxNUVBMDlGNTRERDIwQzVFMTE4RUYB",
                    "receivedMessage": "Hola",
                    "senderPhone": "573217637663",
                    "responseMessage": "Hola! ¿En qué te puedo ayudar hoy?  Te puedo mostrar nuestro catálogo de productos o responder cualquier pregunta que tengas.",
                    "createdAt": "2025-08-06T02:03:37.3268953",
                    "updatedAt": "0001-01-01T00:00:00"
                }
            ]
        }
        ```


4. **Name:** Create message  
**Description:** Save message in database  
**Endpoint:** /Message/CreateMessage  
**Type:** POST
- **Request**
    - **Body:**
        ```json
        {
            "messageId": "wamid.HBgMNTczMjE3NjM3NjYzFQIAEhggODk0MjExMkEyQ0YxNUVBMDlGNTRERDIwQzVFMTE4RUYA",
            "receivedMessage": "Hola",
            "senderPhone": "573217637663",
            "responseMessage": "Hola! ¿En qué te puedo ayudar hoy?  Te puedo mostrar nuestro catálogo de productos o responder cualquier pregunta que tengas."
        }
        ```
- **Response:**
    - **Status code:** 200  
        **Body:**
        ```json
        {
            "success": true,
            "message": "Message successfully created",
            "statusCode": 200,
            "data": {
                "id": "9128977c-dd41-4304-25c2-08ddd48d74ef",
                "messageId": "wamid.HBgMNTczMjE3NjM3NjYzFQIAEhggODk0MjExMkEyQ0YxNUVBMDlGNTRERDIwQzVFMTE4RUYB",
                "receivedMessage": "Hola",
                "senderPhone": "573217637663",
                "responseMessage": "Hola! ¿En qué te puedo ayudar hoy?  Te puedo mostrar nuestro catálogo de productos o responder cualquier pregunta que tengas.",
                "createdAt": "2025-08-06T02:03:37.3268953Z",
                "updatedAt": "0001-01-01T00:00:00"
            }
        }
        ```


5. **Name:** Update message  
**Description:** Update message in database given an id  
**Endpoint:** /Message/UpdateMessage/**{{id}}**  
**Type:** PUT
- **Request**
    - **Params:**
        - **Name:** id  
            **Type:** Path  
            **Example:** 2fdfc9ca-96ed-41a9-79d8-08ddc9857913  
    - **Body:**
        ```json
        {
            "messageId": "wamid.HBgMNTczMjE3NjM3NjYzFQIAEhggODk0MjExMkEyQ0YxNUVBMDlGNTRERDIwQzVFMTE4RUYA",
            "receivedMessage": "Hola",
            "senderPhone": "573217637663",
            "responseMessage": "Hola! ¿En qué te puedo ayudar hoy?  Te puedo mostrar nuestro catálogo de productos o responder cualquier pregunta que tengas."
        }
        ```
- **Response:**
    - **Status code:** 200  
        **Body:**
        ```json
        {
            "success": true,
            "message": "Message with id 9128977c-dd41-4304-25c2-08ddd48d74ef successfully updated",
            "statusCode": 200,
            "data": {
                "id": "9128977c-dd41-4304-25c2-08ddd48d74ef",
                "messageId": "wamid.HBgMNTczMjE3NjM3NjYzFQIAEhggODk0MjExMkEyQ0YxNUVBMDlGNTRERDIwQzVFMTE4RUYB",
                "receivedMessage": "Buenas",
                "senderPhone": "573217637663",
                "responseMessage": "Hola! ¿En qué te puedo ayudar hoy?  Te puedo mostrar nuestro catálogo de productos o responder cualquier pregunta que tengas.",
                "createdAt": "2025-08-06T02:03:37.3268953",
                "updatedAt": "2025-08-06T02:07:27.0465733Z"
            }
        }
        ```


6. **Name:** Delete message  
**Description:** Delete message in database given an id  
**Endpoint:** /Message/DeleteteMessage/**{{id}}**  
**Type:** DELETE
- **Request**
    - **Params:**
        - **Name:** id  
            **Type:** Path  
            **Example:** 2fdfc9ca-96ed-41a9-79d8-08ddc9857913
- **Response:**
    - **Status code:** 200  
    **Body:**
    ```json
    {
        "success": true,
        "message": "Message with id 9128977c-dd41-4304-25c2-08ddd48d74ef successfully deleted",
        "statusCode": 200,
        "data": "9128977c-dd41-4304-25c2-08ddd48d74ef"
    }
    ```