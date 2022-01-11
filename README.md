![build status](https://github.com/isannn/eShopSoa/actions/workflows/ci.yml/badge.svg)

# eShopSoa
Solution for book "Microservices in .NET Core",  1st edition.


## Mocking return data for Product Catalog service

Use public tool - https://app.apiary.io
GET endpoint: https://private-8d38cb-alekseiisaev.apiary-mock.com/products

Put the code below to Apiary editor:

> FORMAT: 1A
> 
> # Author's Name
> 
> Products is a simple API allowing consumers to view product catalog
> 
> \#\# Products [/products]
> 
> \#\#\# List of all products [GET]
> 
> \+ Response 200 (application/json)
> 
>        [
>            {
>                "productId": 1,
>                "productName": "Basic t-shirt",
>                "productDescription": "a quiet t-shirt",
>                "price": { "amount" : 40, "currency": "eur" },
>                "attributes" : [
>                    { 
>                        "sizes": [ "s", "m", "l"],
>                        "colors": ["red", "blue", "green"]
>                    }
>                ]
>            },
>            {
>                "productId": 2,
>                "productName": "Fancy shirt",
>                "productDescription": "a loud t-shirt",
>                "price": { "amount" : 50, "currency": "eur" },
>                "attributes" : [
>                    { 
>                        "sizes": [ "s", "m", "l", "xl"],
>                        "colors": ["ALL", "Batique"]
>                    }
>                ]
>            }
>        ]