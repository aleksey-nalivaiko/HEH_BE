let vendorData = [
    {
        "_id": UUID("e7922459-d83c-4a8d-ba86-6b80e1e55d1d"),
        "name": "Food vendor",
        "links": [
            {
                "url": "website url for food vendor",
                "type": "Website"
            },
            {
                "url": "facebook url for food vendor",
                "type": "Facebook"
            }
        ],
        "mailing": true,
        "phones": [
            {
                "_id": UUID("75e228dc-ee5b-4d81-bf42-6133c5330774"),
                "number": "+375291111111"
            },
            {
                "_id": UUID("18f71e53-def0-4ae8-841c-75a9e871c347"),
                "number": "+375292111111"
            }
        ],
        "addresses": [
            {
                "_id": UUID("0a42474c-775f-43f4-8c7d-fd4873217de5"),
                "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
                "cityId": UUID("021e3664-df32-43f1-8851-f1449cfbc3dd"),
                "street": "street 15"
            },
            {
                "_id": UUID("cf9e5eb1-5125-45ba-a22b-95d4d3ea121f"),
                "countryId": UUID("56346d81-b0ef-4e57-a18e-0b2ca3f04dd9"),
                "cityId": UUID("ea88f894-a912-4a65-a823-392f1bbd514b"),
                "street": "street 23"
            },
            {
                "_id": UUID("231780e4-29e5-4572-aef9-24e6830a0f0d"),
                "countryId": UUID("571ffd86-ae90-4457-b1ec-0ed504076706"),
                "cityId": UUID("4198a349-0f53-4f48-a5ed-766517a26ef3"),
                "street": "street 7"
            }
        ],
        "viewsAmount": 111,
        "email": "food_vendor@gmail.com"
    },
    {
        "_id": UUID("b883835e-4ce3-4134-9811-45a85b6ff113"),
        "name": "Sport and health vendor",
        "links": [
            {
                "url": "vkontakte url for sport and health vendor",
                "type": "Vkontakte"
            }
        ],
        "mailing": true,
        "phones": [
            {
                "_id": UUID("0b89bcbc-6b14-48ce-8946-bb093fc853d8"),
                "number": "+375292222222"
            }
        ],
        "addresses": [
            {
                "_id": UUID("1da47e2f-ca87-4e9f-a905-023b0bc930ab"),
                "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
                "cityId": UUID("021e3664-df32-43f1-8851-f1449cfbc3dd"),
                "street": "street 16"
            },
            {
                "_id": UUID("bc2c741e-190a-4fda-bbac-0bce77aa5ce0"),
                "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
                "cityId": UUID("c77e4c89-57f5-4204-8899-af47545cca22"),
                "street": "street 24"
            },
            {
                "_id": UUID("d78259b3-507b-4f35-98d5-76d04d00f2eb"),
                "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
                "cityId": UUID("021e3664-df32-43f1-8851-f1449cfbc3dd"),
                "street": "street 5"
            }
        ],
        "viewsAmount": 222,
        "email": "sport_and_health_vendor@gmail.com"
    },
    {
        "_id": UUID("a81d4813-0ff3-4a2b-b53c-ebec989ae7fc"),
        "name": "Beauty vendor",
        "links": [
            {
                "url": "vkontakte url for Beauty vendor",
                "type": "Vkontakte"
            }
        ],
        "mailing": false,
        "phones": [
            {
                "_id": UUID("be7ef511-a3c6-4904-9d2f-5e5d58687bcb"),
                "number": "+375293333333"
            }
        ],
        "addresses": [
            {
                "_id": UUID("e52e7a46-08ad-400d-a475-2cf16a20c360"),
                "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
                "cityId": UUID("021e3664-df32-43f1-8851-f1449cfbc3dd"),
                "street": "street 19"
            },
            {
                "_id": UUID("31da3113-b27c-4246-ae72-7f73c567b2b9"),
                "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
                "cityId": UUID("612c1628-f2b7-4553-9232-00bfcd19f95f"),
                "street": "street 9"
            },
            {
                "_id": UUID("ce571d05-a823-4f5c-8de4-0e2316ae3b16"),
                "countryId": UUID("56346d81-b0ef-4e57-a18e-0b2ca3f04dd9"),
                "cityId": UUID("6224eb78-0d71-4299-951e-f756f6a824d2"),
                "street": "street 62"
            }
        ],
        "viewsAmount": 230,
        "email": "beauty_vendor@gmail.com"
    },
    {
        "_id": UUID("91d19b85-6232-4373-b08b-a988d828d27e"),
        "name": "Entertainment vendor",
        "links": [
            {
                "url": "vkontakte url for entertainment vendor",
                "type": "Vkontakte"
            }
        ],
        "mailing": true,
        "phones": [
            {
                "_id": UUID("f6fd6b9b-53af-4ca2-a53d-18d551dd2045"),
                "number": "+375294444444"
            }
        ],
        "addresses": [
            {
                "_id": UUID("2246d903-fcf2-45ca-8f9e-f9226fbe74c9"),
                "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
                "cityId": UUID("021e3664-df32-43f1-8851-f1449cfbc3dd"),
                "street": "street 11"
            },
            {
                "_id": UUID("e2942afa-99e3-45e5-9edd-03dcf6cec6da"),
                "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
                "cityId": UUID("c77e4c89-57f5-4204-8899-af47545cca22"),
                "street": "street 32"
            },
            {
                "_id": UUID("b79bf3d0-38f0-4041-9424-3371a3b89ca8"),
                "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
                "cityId": UUID("021e3664-df32-43f1-8851-f1449cfbc3dd"),
                "street": "street 44"
            }
        ],
        "viewsAmount": 162,
        "email": "entertainment_vendor@gmail.com"
    },
    {
        "_id": UUID("34654c1a-a2c9-4562-abdd-98e62cbcc6f7"),
        "name": "Auto vendor",
        "links": [
            {
                "url": "vkontakte url for auto vendor",
                "type": "Vkontakte"
            }
        ],
        "mailing": true,
        "phones": [
            {
                "_id": UUID("dc9598e2-eb40-4bd7-ac31-1014900e72d8"),
                "number": "+375295555555"
            }
        ],
        "addresses": [
            {
                "_id": UUID("f868212b-bbf3-4a68-a13c-7459b9eeeabb"),
                "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
                "cityId": UUID("021e3664-df32-43f1-8851-f1449cfbc3dd"),
                "street": "street 55"
            },
            {
                "_id": UUID("0f45a222-af0a-4536-a88d-8f2b2a027fdd"),
                "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
                "cityId": UUID("c77e4c89-57f5-4204-8899-af47545cca22"),
                "street": "street 13"
            },
            {
                "_id": UUID("d43680d8-9460-4fbe-ac99-c57f8fe7fdfa"),
                "countryId": UUID("56346d81-b0ef-4e57-a18e-0b2ca3f04dd9"),
                "cityId": UUID("ea88f894-a912-4a65-a823-392f1bbd514b"),
                "street": "street 44"
            },
            {
                "_id": UUID("3ff48d15-4a51-4081-8413-6fd18da644e6"),
                "countryId": UUID("56346d81-b0ef-4e57-a18e-0b2ca3f04dd9"),
                "cityId": UUID("6224eb78-0d71-4299-951e-f756f6a824d2"),
                "street": "street 6"
            }
        ],
        "viewsAmount": 366,
        "email": "auto_vendor@gmail.com"
    },
    {
        "_id": UUID("72ff6421-f6c6-4d55-bf54-f9833723f30f"),
        "name": "Travel vendor",
        "links": [
            {
                "url": "vkontakte url for travel vendor",
                "type": "Vkontakte"
            }
        ],
        "mailing": true,
        "phones": [
            {
                "_id": UUID("5071092e-e7b0-491f-813e-8113071015ab"),
                "number": "+375296666666"
            }
        ],
        "addresses": [
            {
                "_id": UUID("bd4cb704-0b02-4f62-a421-8c62e312e797"),
                "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
                "cityId": UUID("021e3664-df32-43f1-8851-f1449cfbc3dd"),
                "street": "street 18"
            },
            {
                "_id": UUID("ddb70131-845b-4d64-bf3f-4427dfa3fd2d"),
                "countryId": UUID("571ffd86-ae90-4457-b1ec-0ed504076706"),
                "cityId": UUID("4198a349-0f53-4f48-a5ed-766517a26ef3"),
                "street": "street 6"
            },
            {
                "_id": UUID("c05e1b31-9c68-4ca1-abfe-4cff42f2a2d3"),
                "countryId": UUID("56346d81-b0ef-4e57-a18e-0b2ca3f04dd9"),
                "cityId": UUID("ea88f894-a912-4a65-a823-392f1bbd514b"),
                "street": "street 12"
            },
            {
                "_id": UUID("5ce5710e-f900-4edd-a047-06cea7f244a9"),
                "countryId": UUID("571ffd86-ae90-4457-b1ec-0ed504076706"),
                "cityId": UUID("23fde963-54e5-4991-a0a9-d5013550703c"),
                "street": "street 6"
            }
        ],
        "viewsAmount": 167,
        "email": "travel_vendor@gmail.com"
    },
    {
        "_id": UUID("b2f9bca3-bd3a-4fa1-874b-b17025a05873"),
        "name": "Education vendor",
        "links": [
            {
                "url": "vkontakte url for education vendor",
                "type": "Vkontakte"
            }
        ],
        "mailing": false,
        "phones": [
            {
                "_id": UUID("9e5d1d27-3196-4102-818d-8c2c48bc428a"),
                "number": "+375297777777"
            }
        ],
        "addresses": [
            {
                "_id": UUID("5fd5796b-a71b-415e-9492-169e16d7502d"),
                "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
                "cityId": UUID("021e3664-df32-43f1-8851-f1449cfbc3dd"),
                "street": "street 17"
            },
            {
                "_id": UUID("7d9d27ec-d45d-435f-8f05-6c2f7c5a8e28"),
                "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
                "cityId": UUID("c77e4c89-57f5-4204-8899-af47545cca22"),
                "street": "street 12"
            },
            {
                "_id": UUID("a1b4f74c-0e9d-4e3c-ac10-bb3414e29751"),
                "countryId": UUID("571ffd86-ae90-4457-b1ec-0ed504076706"),
                "cityId": UUID("23fde963-54e5-4991-a0a9-d5013550703c"),
                "street": "street 32"
            },
            {
                "_id": UUID("05d7d671-9b63-4ccc-b4ed-fed75ffbb722"),
                "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
                "cityId": UUID("91268c45-0b69-40b8-8c03-bab2fdc5f431"),
                "street": "street 32"
            },
            {
                "_id": UUID("4f381502-d37c-4968-8fc5-c6980270027f"),
                "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
                "cityId": UUID("021e3664-df32-43f1-8851-f1449cfbc3dd"),
                "street": "street 32"
            }
        ],
        "viewsAmount": 289,
        "email": "education@gmail.com"
    }
]