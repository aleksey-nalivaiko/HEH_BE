﻿let discountData = [
    {
        "_id": UUID("f1aac79e-f684-42f2-8ff9-8613b2541878"),
        "conditions": "30% by promocode",
        "tagsIds": [UUID("d1adffea-945d-44c6-a79d-b935ccec3051"),
        UUID("7de99c4f-d795-4a28-a7cf-1e2f952c71ee"),
        UUID("7bb365ae-87d7-458d-b170-00ceabea9c54")
        ],
        "vendorId": UUID("e7922459-d83c-4a8d-ba86-6b80e1e55d1d"),
        "vendorName": "Mcdonalds",
        "promoCode": "Mcfood",
        "addresses": [
            {
                "_id": UUID("0a42474c-775f-43f4-8c7d-fd4873217de5"),
                "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
                "cityId": UUID("021e3664-df32-43f1-8851-f1449cfbc3dd"),
                "street": "pr. Dzerzhinskogo, 96"
            },
            {
                "_id": UUID("cf9e5eb1-5125-45ba-a22b-95d4d3ea121f"),
                "countryId": UUID("56346d81-b0ef-4e57-a18e-0b2ca3f04dd9"),
                "cityId": UUID("ea88f894-a912-4a65-a823-392f1bbd514b"),
                "street": "ul. Nemiga, 12B"
            },
            {
                "_id": UUID("231780e4-29e5-4572-aef9-24e6830a0f0d"),
                "countryId": UUID("571ffd86-ae90-4457-b1ec-0ed504076706"),
                "cityId": UUID("4198a349-0f53-4f48-a5ed-766517a26ef3"),
                "street": "pr. Nezavisimosti, 23"
            }
        ],
        "phones": [
            {
                "_id": UUID("75e228dc-ee5b-4d81-bf42-6133c5330774"),
                "number": "+375172700047"
            },
            {
                "_id": UUID("18f71e53-def0-4ae8-841c-75a9e871c347"),
                "number": "+375172178471"
            }
        ],
        "startDate": "2021-02-03T18:30:25",
        "endDate": "2021-03-03T18:30:25",
        "categoryId": UUID("ab067429-6fbf-4798-92bf-d62012db8f25")
    },
    {
        "_id": UUID("509ce980-e780-42ca-a982-4d33c0b9dfa8"),
        "conditions": "10% by promocode",
        "tagsIds": [
            UUID("3d9fb1b9-c326-4a85-986f-237cabc0e368"),
            UUID("45fd014b-1389-4036-824a-7dd11c43d1cd"),
            UUID("055a0b7f-dfed-4e7a-9ea4-0e380bb6dcfb")
        ],
        "vendorId": UUID("b883835e-4ce3-4134-9811-45a85b6ff113"),
        "vendorName": "Max Mirnyi Center",
        "promoCode": "Tenis",
        "addresses": [
            {
                "_id": UUID("1da47e2f-ca87-4e9f-a905-023b0bc930ab"),
                "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
                "cityId": UUID("021e3664-df32-43f1-8851-f1449cfbc3dd"),
                "street": "ul. Gromova 14"
            }
        ],
        "phones": [
            {
                "_id": UUID("0b89bcbc-6b14-48ce-8946-bb093fc853d8"),
                "number": "+375293406506"
            }
        ],
        "startDate": "2021-10-03T18:30:25",
        "endDate": "2021-10-03T18:30:25",
        "categoryId": UUID("751dc6e4-603b-477f-91dc-97c09d13d96a")
    },
    {
        "_id": UUID("d4ece352-2344-45c3-aa22-bd8ff94e5d7e"),
        "conditions": "10% by promocode",
        "tagsIds": [
            UUID("bfd824c2-0f9d-4b76-848f-37828c161d9e"),
            UUID("6bf3f73e-c844-4cd5-a3bc-6f3364530468")
        ],
        "vendorId": UUID("a81d4813-0ff3-4a2b-b53c-ebec989ae7fc"),
        "vendorName": "Beautycenter O2",
        "promoCode": "nails",
        "addresses": [
            {
                "_id": UUID("e52e7a46-08ad-400d-a475-2cf16a20c360"),
                "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
                "cityId": UUID("021e3664-df32-43f1-8851-f1449cfbc3dd"),
                "street": "ul.Zybitskaya 6-32"
            }
        ],
        "phones": [
            {
                "_id": UUID("be7ef511-a3c6-4904-9d2f-5e5d58687bcb"),
                "number": "+375298819819"
            }
        ],
        "startDate": "2021-10-03T18:30:25",
        "endDate": "2021-10-03T18:30:25",
        "categoryId": UUID("11f8463a-827f-4873-8550-62e0ee03b369")
    },
    {
        "_id": UUID("e0d69154-4d2e-463d-b8e7-f497489e0751"),
        "conditions": "15% by promocode",
        "tagsIds": [
            UUID("bb5fd66d-63d0-4dde-8580-7a26771bc129"),
            UUID("7f7b40be-7c47-4866-a867-b60504c4bb4f")
        ],
        "vendorId": UUID("91d19b85-6232-4373-b08b-a988d828d27e"),
        "vendorName": "Dinozavria",
        "promoCode": "DinosorAndI",
        "addresses": [
            {
                "_id": UUID("2246d903-fcf2-45ca-8f9e-f9226fbe74c9"),
                "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
                "cityId": UUID("021e3664-df32-43f1-8851-f1449cfbc3dd"),
                "street": "ul. Pritytskogo 29"
            }
        ],
        "phones": [
            {
                "_id": UUID("f6fd6b9b-53af-4ca2-a53d-18d551dd2045"),
                "number": "+375445429429"
            }
        ],
        "startDate": "2021-10-03T18:30:25",
        "endDate": "2021-10-03T18:30:25",
        "categoryId": UUID("8f411964-f934-4302-bdd0-db2fc2a549e7")
    },
    {
        "_id": UUID("ec82d67f-1371-4a69-98c9-ef425ba2e47d"),
        "conditions": "3% by promocode",
        "tagsIds": [
            UUID("d1575ac0-cedf-49fd-9fb5-18579878720d"),
            UUID("76dce0ac-3d3d-49cd-8f15-fbe43ebe9900")
        ],
        "vendorId": UUID("34654c1a-a2c9-4562-abdd-98e62cbcc6f7"),
        "vendorName": "Autocenter Citroen",
        "promoCode": "Auto",
        "addresses": [
            {
                "_id": UUID("f868212b-bbf3-4a68-a13c-7459b9eeeabb"),
                "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
                "cityId": UUID("021e3664-df32-43f1-8851-f1449cfbc3dd"),
                "street": "ul. Kolesnikova 38"
            },
            {
                "_id": UUID("0f45a222-af0a-4536-a88d-8f2b2a027fdd"),
                "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
                "cityId": UUID("c77e4c89-57f5-4204-8899-af47545cca22"),
                "street": "ul. Kol'tsova 48"
            },
            {
                "_id": UUID("d43680d8-9460-4fbe-ac99-c57f8fe7fdfa"),
                "countryId": UUID("56346d81-b0ef-4e57-a18e-0b2ca3f04dd9"),
                "cityId": UUID("ea88f894-a912-4a65-a823-392f1bbd514b"),
                "street": "ul. Lazurnaya 13a"
            }
        ],
        "phones": [
            {
                "_id": UUID("dc9598e2-eb40-4bd7-ac31-1014900e72d8"),
                "number": "+375173369955"
            }
        ],
        "startDate": "2021-10-03T18:30:25",
        "endDate": "2021-10-03T18:30:25",
        "categoryId": UUID("1c22cbf6-9692-4207-83e3-8d51a6bbd8be")
    },
    {
        "_id": UUID("3e387bce-a5e5-4a97-9647-6c5fa3645f1b"),
        "conditions": "15% by promocode",
        "tagsIds": [
            UUID("0998631c-eb37-4767-8ff7-12bc8166dfae"),
            UUID("58e6f3d3-a1c4-488f-96a7-325824993816")
        ],
        "vendorId": UUID("72ff6421-f6c6-4d55-bf54-f9833723f30f"),
        "vendorName": "Booking",
        "promoCode": "Travel",
        "addresses": [
        ],
        "phones": [
            {
                "_id": UUID("5071092e-e7b0-491f-813e-8113071015ab"),
                "number": "+375296666666"
            }
        ],
        "startDate": "2021-10-03T18:30:25",
        "endDate": "2021-10-03T18:30:25",
        "categoryId": UUID("97c8c966-634a-4a3a-b87b-89d2d31587d3")
    },
    {
        "_id": UUID("b0697535-7071-4380-80d1-15556203dd0c"),
        "conditions": "7% by promocode",
        "tagsIds": [
            UUID("42a69c2c-d3a0-4ec8-b0ef-5319bc27538f"),
            UUID("3d9590bb-49b6-4f08-b717-264c34ec6d26")
        ],
        "vendorId": UUID("b2f9bca3-bd3a-4fa1-874b-b17025a05873"),
        "vendorName": "It-academy",
        "promoCode": "Auto",
        "addresses": [
            {
                "_id": UUID("5fd5796b-a71b-415e-9492-169e16d7502d"),
                "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
                "cityId": UUID("021e3664-df32-43f1-8851-f1449cfbc3dd"),
                "street": "ul. Skryganova 14"
            }
        ],
        "phones": [
            {
                "_id": UUID("9e5d1d27-3196-4102-818d-8c2c48bc428a"),
                "number": "+375297815370"
            }
        ],
        "startDate": "2021-10-03T18:30:25",
        "endDate": "2021-10-03T18:30:25",
        "categoryId": UUID("66490a95-4e19-4cea-9c60-a12031bd330c")
    }
]