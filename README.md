# Payment Gateway API #

## Purpose
The Payment Gateway API allows a merchant to offer a way for their shoppers to pay for their product. The API also allows retrieving details of previously made payments, which will help the merchant with their reconciliation and reporting needs.

## Code Architecture
We have a .NET Core API project `Checkout.Payment.Gateway.Api` which follows the following code-level hierarchy and flow.
- **Validator**: Validates request parameters (mandatory fields)
    - **Controller**: Performs request execution
        - **PaymentService**: Performs payment logic for storing, quering and forwarging payment request to acquiring bank
            - **AcquiringBank**: Performs the client call to acquiring bank and builds retrieved response to domain model 
            - **PaymentRepository**: Performs data store operations for payment requests
        - **ResponseBuilder**: Builds response for a consumer 

<img src="docs/diagrams/requests-pipeline.png">


# Create Payment
POST https://api.checkout.com/payment

REQUEST BODY (required)
```
{
    "shopperId": "8cb389ba-64dc-4f77-9250-b0ea046d9273",
    "merchantId": "52d59ba9-0fb2-4ca5-82a1-efa3f17fe12c",
    "currency": "gbp",
    "amount": 156.60,
    "cardDetails": {
        "nameOnCard": "Vladimirs Paskevicus",
        "cardNumber": "1243123412341234",
        "expirationMonth": 3,
        "expirationYear": 2027,
        "securityCode": 555
    }
}
```
### Request properties and validation requirement
PaymentRequest
|           Property |        Type |                   Validation Requirement |
|-------------------:|------------:|------------------------------------------|
|           shopperId|         Guid| Required
|          merchantId|         Guid| Required
|            currency|       string| Required, length of 3 characters
|              amount|      decimal| Required
|  shopperCardDetails|       object| Required

CardDetails
|           Property |        Type |                   Validation Requirement |
|-------------------:|------------:|------------------------------------------|
|          nameOnCard|       string| Required, length between 5 and 70
|          cardNumber|       string| Required, numeric with length of 16 
|     expirationMonth|          int| Required, range between 1 and 12
|      expirationYear|          int| Required, numeric with length of 4, starting from current year 
|        securityCode|          int| Required, range between 100 and 999

# Responses
## 200 OK - RESPONSE BODY
```
{
    "title": "Payment processed",
    "status": 200,
    "payment": {
        Id: "6e8e4b2e-fb16-4d22-92dd-ca6d7c903e18",
        statusCode: 001
    }
}
```
### Payment Transaction Response Codes
|       ResponseCode |    Response Message | Response Type                            |
|-------------------:|--------------------:|------------------------------------------|
|                 001|             Complete| The payment transaction is complete
|                 002|             Approved| The transaction to be processed
|                 003|             Declined| The payment is decliened 

## 400 Bad Request - RESPONSE BODY
```
{
	"title": "One or more validation errors occured."
	"status": 400
    "errors": [
        "Shopper id is required",
        "Merchant id is required"
    	"Currency is required",
  		"Amount is required",
    	"Name on card is required",
  		"Card number is required",
    	"Expiration month is required",
  		"Expiration year is required",
    	"Security code is required"
    ]
}
```
## 500 Internal Server Error - RESPONSE BODY
```
{
	"title": "Service unavailable"
	"status": 500
}
```

# Get Payment #
GET https://api.checkout.com/payment/4ac61dd6-4a9f-4f7d-8385-0263ad2d0123

## 200 OK - RESPONSE BODY
```
{
    "payment": {
        Id: "6e8e4b2e-fb16-4d22-92dd-ca6d7c903e18"
        statusCode: 001
    },
    "shopperId": "8cb389ba-64dc-4f77-9250-b0ea046d9273",
    "merchantId": "52d59ba9-0fb2-4ca5-82a1-efa3f17fe12c",
    "dateCreated": "2023-05-11T10:36:26.988Z",
    "currency": "gbp",
    "amount": 156.60,
    "cardDetails": {
        "nameOnCard": "Vladimirs Paskevicus",
        "cardNumber": "**** **** **** 1234",
        "expirationMonth": 3,
        "expirationYear": 2027,
        "securityCode": 555
    }
}
```
## 404 Not Found - RESPONSE BODY
```
{
	"title": "Not Found"
	"status": 404
}
```
## 500 Internal Server Error - RESPONSE BODY
```
{
	"title": "Service unavailable"
	"status": 500
}
```