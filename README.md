# TrovGildedRoseService

a) Choice of data format?
 The API was built using WebAPI and it can support several data formats such as Json, XML,text and HTML.
However, I prefer to represent my data in Json due to the following reasons:

* It is faster beacuse it is a light-weight text-based format
* It is a language-Independent format, almost all programming language can parse and generate JSON data. Hence, making it easy to to consume 
* It has wide range of supported browser compatibility

SAMPLE REQUEST AND RESPONSE
****** GetItemList *****
Request: Header
Authorization: bearer r94kpNfEBk2tyDV9A6QNPNaOvI_0mt0BsnpZZC67PR5f4rNKZ1cTG8rAotR3KIu5sGlBmnYJnL89miOmSc9m_twDNnETf6Q_w2Fty7QGBL3M-ynUmLSUJG4nunEDQI2orfQoV4oYjiCVquaZVZAWKngXvew7Jaux6H8678iJm4Xz9lVkmRmmF7iHG6WVodKWMTlecTWiuvveREKm7l0IOIwHyua2A2oVeI6qzIs6MM5YEFCLrDBxgZi0ECEJ6whuJqtigyOkg0EuY2PyYsKj4Ubzfsf7UzXoDgVkG0w49Lp7ULX-YeB8klyKgpfoCbTr

Response:
{
    "responseCode": "00",
    "responseMessage": "Successful",
    "result": [
        {
            "Name": "Planks",
            "Description": "Log of wood",
            "Price": 3000
        },
        {
            "Name": "Sofa",
            "Description": "Furniture",
            "Price": 500
        },
        {
            "Name": "Flower Vase",
            "Description": "Home decoration",
            "Price": 250
        }
    ]
}

****** BuyItems *****
REQUEST:
Header::

Authorization: bearer dQC7MdyArpEv4bFeiCl5g9MPfdEBZ6GVKsUt3fKLfQYbagjYN0F7Ctkloo6QGHVQEflrwf4u0CrjpAziGN2ZdWKVCboPqQToOqAxgJnY6LAuhukItZepzyJ0xXBV3k36QsUgqSUi-sLEOMBUDiSxa6sbsjQRV34SGFPEcl3D8ymzwP7646BhQXIE7eLvbEtf7xNv4iYecv1G4-ExHm2yEnevsKz_gDUYDQHESTyS9HJ_oqAYeAB9UQ1hXPe8s5esNXfIPtoK68Rzh6shuXM5m2LzUeVubNFe21RjrhqbQ6aZSfi50QXIJyjBzyVe0vrr
Content-Type:application/x-www-form-urlencoded

Body::
ItemName:Planks
Quantity:500
ReceiptRef:Ref1234
DeliveryLocation:California, USA
timestamp:1553644809379
HashValue:jeB9FrZbmREq0BkvV+1Upb3huITcO9Vq2ChGTPTu1Is=


Response::
{
    "ResponseCode": "00",
    "ResponseMessage": "Payment Successful",
    "ReferenceNo": "XXXXXX"
}


b) The authentication mechanism used is Bearer Token with Oauth using the username and password created for each merchants to manage their profile details (User detail was hardcoded for the sake for the sample app).Token based authentication is secured and can make use of the existing user credentials maintained. However, since it is transmitted via HTTP and it is unsecured, an added security is included for the buyItem endpoint, this makes use of an API Key and Secret key shared and known only by the merchant and the shopkeeper.
A base64 SHA256 hash will be generated from the concatenation of the "secretkey,timestamp,apikey,recieptref"
Note: timestamp is the request timespan in milliseconds and the recieptref is the reference of the payment receipt(As it is assumed that payment has been made and receipt generated outside the API)
The hashvalue is added as part of the request and validated for every call to BuyItem, this is set to expire every minute, henceclient consuming the service will be required to generate a new token for every request. 

