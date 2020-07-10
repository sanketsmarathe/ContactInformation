--------------------------------------------------------------------------------------------
API List:
--------------------------------------------------------------------------------------------
1: Add Contact

POST: https://localhost:44390/api/contact/add

Request:
{
    "firstName": "Sanket",
    "lastName": "Marathe",
    "email": "sanket@marathe.com",
    "phoneNumber": "9876543210",
    "status": "Active"
}

Response:
{
    "success": true,
    "errors": null,
    "result": {
        "contactId": "0adaadad-335b-48b5-0762-08d8249f2027",
        "userId": "d02f7514-37a9-4698-8bb0-70e62bbde1de",
        "firstName": "Sanket",
        "lastName": "Marathe",
        "email": "sanket@marathe.com",
        "phoneNumber": "9876543210",
        "status": "Active"
    }
}
--------------------------------------------------------------------------------------------
2. Update Contact

PUT: https://localhost:44390/api/contact/update/0adaadad-335b-48b5-0762-08d8249f2027

Request:
{
    "firstName": "Sanket",
    "lastName": "Marathe",
    "email": "sanket@sanket.com",
    "phoneNumber": "9876543211",
    "status": "Active"
}

Response:
{
    "success": true,
    "errors": null,
    "result": {
        "contactId": "0adaadad-335b-48b5-0762-08d8249f2027",
        "userId": "d02f7514-37a9-4698-8bb0-70e62bbde1de",
        "firstName": "Sanket",
        "lastName": "Marathe",
        "email": "sanket@sanket.com",
        "phoneNumber": "9876543211",
        "status": "Active"
    }
}
--------------------------------------------------------------------------------------------
3. List of Contact

GET: https://localhost:44390/api/contact/all?page=1&pageSize=10

Response:
{
    "success": true,
    "errors": null,
    "result": [
        {
            "contactId": "0adaadad-335b-48b5-0762-08d8249f2027",
            "userId": "d02f7514-37a9-4698-8bb0-70e62bbde1de",
            "firstName": "Sanket",
            "lastName": "Marathe",
            "email": "sanket@sanket.com",
            "phoneNumber": "9876543211",
            "status": "Active"
        }
    ]
}
--------------------------------------------------------------------------------------------
4. Delete Contact

DELETE: https://localhost:44390/api/contact/delete/0adaadad-335b-48b5-0762-08d8249f2027

Response:
{
    "success": true,
    "errors": null,
    "result": {
        "contactId": "0adaadad-335b-48b5-0762-08d8249f2027",
        "id": "d02f7514-37a9-4698-8bb0-70e62bbde1de",
        "status": "InActive",
        "user": null,
        "createdBy": "d02f7514-37a9-4698-8bb0-70e62bbde1de",
        "createdAt": "2020-07-10T07:01:51.29272",
        "updatedBy": "d02f7514-37a9-4698-8bb0-70e62bbde1de",
        "updatedAt": "2020-07-10T07:15:07.6292962Z"
    }
}
--------------------------------------------------------------------------------------------
