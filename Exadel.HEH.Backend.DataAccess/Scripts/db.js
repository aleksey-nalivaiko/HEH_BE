{
    let userData = [
        {
            "_id": UUID("6dead3f8-599e-11eb-ae93-0242ac130002"),
            "role": "moderator",
            "name": "Mary Jones",
            "email": "maryj@exadel.com",
            "password": "a5Adj7O7",
            "address": {
                "country": "Belarus",
                "city": "Minsk",
                "street": "street 1"
            },
            "isActive": true,
            "categoryNotifications": [
                {
                    "categoryId": UUID("fb42cd13-1c15-4388-bb3d-216376b5a225")
                },
                {
                    "categoryId": UUID("a3ab67b9-1a72-48e1-a928-e0dbe0b17a5c")
                },
                {
                    "categoryId": UUID("c86bec9f-2ef9-4be2-bd21-af14aa0d187f")
                }
            ],
            "tagNotifications": [
                {
                    "tagId": UUID("ab4b5672-e2f6-4935-ba03-58851d9c5991")
                },
                {
                    "tagId": UUID("22427389-7127-41b4-9aa4-af2078980cf8")
                }
            ],
            "vendorNotifications": [
                {
                    "vendorId": UUID("662c27f1-8f63-44ac-a873-aac6f09ab173")
                }
            ],
            "newVendorNotificationIsOn": true,
            "newDiscountNotificationIsOn": true,
            "hotDiscountsNotificationIsOn": false,
            "cityChangeNotificationIsOn": true,
            "favorites": [
                {
                    "discountId": UUID("5ff9732f-8187-4b14-92d7-89bd72cd91ba"),
                    "note": "note for discount 1"
                },
                {
                    "discountId": UUID("06734050-5268-4540-91d0-8a4e9a7af6a0"),
                    "note": "note for discount 2"
                }
            ]
        },
        {
            "_id": UUID("c8fd9ed0-59a1-11eb-ae93-0242ac130002"),
            "role": "administrator",
            "name": "Jack Smith",
            "email": "jacks@exadel.com",
            "password": "895sdj9O765",
            "address": {
                "country": "Belarus",
                "city": "Minsk",
                "street": "street 2"
            },
            "isActive": true,
            "categoryNotifications": [
                {
                    "categoryId": UUID("a3ab67b9-1a72-48e1-a928-e0dbe0b17a5c")
                },
                {
                    "categoryId": UUID("c86bec9f-2ef9-4be2-bd21-af14aa0d187f")
                }
            ],
            "tagNotifications": [
                {
                    "tagId": UUID("81cf086a-84a8-4291-9a55-791f1005f09e")
                },
                {
                    "tagId": UUID("ab4b5672-e2f6-4935-ba03-58851d9c5991")
                }
            ],
            "vendorNotifications": [
                {
                    "vendorId": UUID("ef809bbf-1ff7-4f1c-ac2b-ab5040294cf1")
                }
            ],
            "newVendorNotificationIsOn": true,
            "newDiscountNotificationIsOn": true,
            "hotDiscountsNotificationIsOn": false,
            "cityChangeNotificationIsOn": true,
            "favorites": [
                {
                    "discountId": UUID("5ff9732f-8187-4b14-92d7-89bd72cd91ba"),
                    "note": "note for discount 1"
                },
                {
                    "discountId": UUID("06734050-5268-4540-91d0-8a4e9a7af6a0"),
                    "note": "note for discount 2"
                }
            ]
        },
        {
            "_id": UUID("a7df950e-59a2-11eb-ae93-0242ac130002"),
            "role": "employee",
            "name": "Alex Rollings",
            "email": "alexr@exadel.com",
            "password": "5875soj9O765",
            "address": {
                "country": "Belarus",
                "city": "Minsk",
                "street": "street 3"
            },
            "isActive": true,
            "categoryNotifications": [
                {
                    "categoryId": UUID("a3ab67b9-1a72-48e1-a928-e0dbe0b17a5c")
                },
                {
                    "categoryId": UUID("c86bec9f-2ef9-4be2-bd21-af14aa0d187f")
                }
            ],
            "tagNotifications": [
                {
                    "tagId": UUID("81cf086a-84a8-4291-9a55-791f1005f09e")
                },
                {
                    "tagId": UUID("ab4b5672-e2f6-4935-ba03-58851d9c5991")
                }
            ],
            "vendorNotifications": [
                {
                    "vendorId": UUID("ef809bbf-1ff7-4f1c-ac2b-ab5040294cf1")
                }
            ],
            "newVendorNotificationIsOn": true,
            "newDiscountNotificationIsOn": true,
            "hotDiscountsNotificationIsOn": false,
            "cityChangeNotificationIsOn": true,
            "favorites": [
                {
                    "discountId": UUID("5ff9732f-8187-4b14-92d7-89bd72cd91ba"),
                    "note": "note for discount 1"
                },
                {
                    "discountId": UUID("06734050-5268-4540-91d0-8a4e9a7af6a0"),
                    "note": "note for discount 2"
                }
            ]
        },
        {
            "_id": UUID("662db544-59a4-11eb-ae93-0242ac130002"),
            "role": "employee",
            "name": "Alex Rollings",
            "email": "alexr@exadel.com",
            "password": "5875soj9O765",
            "address": {
                "country": "Belarus",
                "city": "Minsk",
                "street": "street 4"
            },
            "isActive": false,
            "categoryNotifications": [
                {
                    "categoryId": UUID("a3ab67b9-1a72-48e1-a928-e0dbe0b17a5c")
                },
                {
                    "categoryId": UUID("c86bec9f-2ef9-4be2-bd21-af14aa0d187f")
                }
            ],
            "tagNotifications": [
                {
                    "tagId": UUID("81cf086a-84a8-4291-9a55-791f1005f09e")
                },
                {
                    "tagId": UUID("ab4b5672-e2f6-4935-ba03-58851d9c5991")
                }
            ],
            "vendorNotifications": [
                {
                    "vendorId": UUID("ef809bbf-1ff7-4f1c-ac2b-ab5040294cf1")

                }
            ],
            "newVendorNotificationIsOn": true,
            "newDiscountNotificationIsOn": true,
            "hotDiscountsNotificationIsOn": false,
            "cityChangeNotificationIsOn": true,
            "favorites": [
                {
                    "discountId": UUID("5ff9732f-8187-4b14-92d7-89bd72cd91ba"),
                    "note": "note for discount 1"
                },
                {
                    "discountId": UUID("06734050-5268-4540-91d0-8a4e9a7af6a0"),
                    "note": "note for discount 2"
                }
            ]
        }
    ];

    let historyData = [
        {
            "_id": UUID("209271f8-59b5-11eb-ae93-0242ac130002"),
            "userId": UUID("6dead3f8-599e-11eb-ae93-0242ac130002"),
            "role": "moderator",
            "name": "Mary Jones",
            "email": "maryj@exadel.com",
            "action": "add new vendor: Dominos",
            "date": new Date("2021-01-18T16:00:00Z")
        },
        {
            "_id": UUID("1c2ec8c8-59b5-11eb-ae93-0242ac130002"),
            "userId": UUID("6dead3f8-599e-11eb-ae93-0242ac130002"),
            "role": "moderator",
            "name": "Mary Jones",
            "email": "maryj@exadel.com",
            "action": "add new discount: 10% for second pizza",
            "date": new Date("2021-01-18T16:10:12Z")
        },
        {
            "_id": UUID("178a3744-59b5-11eb-ae93-0242ac130002"),
            "userId": UUID("c8fd9ed0-59a1-11eb-ae93-0242ac130002"),
            "role": "administrator",
            "name": "Jack Smith",
            "email": "jacks@exadel.com",
            "action": "remove discount: 70% for second pizza",
            "date": new Date("2021-01-17T13:56:45Z")
        }
    ];

    let discountData = [
        {
            "_id": UUID("06734050-5268-4540-91d0-8a4e9a7af6a0"),
            "conditions": "Conditions string",
            "tagsIds": [
                {
                    "tag_id": UUID("8134e0e7-bd6f-4aa2-82de-b6301fd2979f"),
                    "name": "tag name 1",
                    "categoryId": UUID("fb42cd13-1c15-4388-bb3d-216376b5a225")
                },
                {
                    "tag_id": UUID("81cf086a-84a8-4291-9a55-791f1005f09e"),
                    "name": "tag name 2",
                    "categoryId": UUID("a3ab67b9-1a72-48e1-a928-e0dbe0b17a5c")
                }
            ],
            "vendorId": UUID("662c27f1-8f63-44ac-a873-aac6f09ab173"),
            "promoCode": "promo code 1",
            "address": [
                {
                    "country": "Belarus",
                    "city": "Minsk",
                    "street": "street 1"
                },
                {
                    "country": "Belarus",
                    "city": "Minsk",
                    "street": "street 2"
                }
            ],
            "startDate": new Date("2021-01-18"),
            "endDate": new Date("2021-01-22"),
            "categoryId": UUID("fb42cd13-1c15-4388-bb3d-216376b5a225")
        },
        {
            "_id": UUID("5ff9732f-8187-4b14-92d7-89bd72cd91ba"),
            "conditions": "Conditions string",
            "tagsIds": [
                UUID("8134e0e7-bd6f-4aa2-82de-b6301fd2979f"),
                UUID("81cf086a-84a8-4291-9a55-791f1005f09e")
            ],
            "vendorId": UUID("ef809bbf-1ff7-4f1c-ac2b-ab5040294cf1"),
            "promoCode": "promo code 1",
            "address": [
                {
                    "country": "Belarus",
                    "city": "Minsk",
                    "street": "street 1"
                },
                {
                    "country": "Belarus",
                    "city": "Minsk",
                    "street": "street 2"
                }
            ],
            "startDate": new Date("2021-01-18"),
            "endDate": new Date("2021-01-22"),
            "categoryId": UUID("fb42cd13-1c15-4388-bb3d-216376b5a225")
        }
    ];

    let vendorData = [
        {
            "_id": UUID("662c27f1-8f63-44ac-a873-aac6f09ab173"),
            "name": "vendor name 1",
            "links": [
                {
                    "url": "url1",
                    "type": "site"
                },
                {
                    "url": "url2",
                    "type": "facebook"
                }
            ],
            "mailing": true,
            "phone": "+375291111111",
            "viewsAmount": "111",
            "email": "vendor@gmail.com"
        },
        {
            "_id": UUID("ef809bbf-1ff7-4f1c-ac2b-ab5040294cf1"),
            "name": "vendor name 1",
            "links": [
                {
                    "url": "url1",
                    "type": "site"
                },
                {
                    "url": "url2",
                    "type": "instagram"
                }
            ],
            "mailing": true,
            "phone": "+375291111111",
            "viewsAmount": "111",
            "email": "vendor@gmail.com"
        }
    ];

    let tagData = [
        {
            "_id": UUID("8134e0e7-bd6f-4aa2-82de-b6301fd2979f"),
            "name": "pizza",
            "categoryId": UUID("fb42cd13-1c15-4388-bb3d-216376b5a225")
        },
        {
            "_id": UUID("81cf086a-84a8-4291-9a55-791f1005f09e"),
            "name": "sushi",
            "categoryId": UUID("fb42cd13-1c15-4388-bb3d-216376b5a225")
        },
        {
            "_id": UUID("ab4b5672-e2f6-4935-ba03-58851d9c5991"),
            "name": "burger",
            "categoryId": UUID("fb42cd13-1c15-4388-bb3d-216376b5a225")
        },
        {
            "_id": UUID("22427389-7127-41b4-9aa4-af2078980cf8"),
            "name": "coffee",
            "categoryId": UUID("fb42cd13-1c15-4388-bb3d-216376b5a225")
        }
    ];

    let categoryData = [
        {
            "_id": UUID("fb42cd13-1c15-4388-bb3d-216376b5a225"),
            "name": "food"
        },
        {
            "_id": UUID("a3ab67b9-1a72-48e1-a928-e0dbe0b17a5c"),
            "name": "sport"
        },
        {
            "_id": UUID("adbab91e-942b-4bc2-992f-19b6877b1e7d"),
            "name": "beauty"
        },
        {
            "_id": UUID("c86bec9f-2ef9-4be2-bd21-af14aa0d187f"),
            "name": "health"
        }
    ];

    let preOrderData = [
        {
            "_id": UUID("00a09510-2e0f-4044-90a9-7f880da67066"),
            "userId": UUID("6dead3f8-599e-11eb-ae93-0242ac130002"),
            "discountId": UUID("5ff9732f-8187-4b14-92d7-89bd72cd91ba"),
            "orderDateTime": new Date("2021-01-17T13:56:45Z"),
            "info": "some comment"
        },
        {
            "_id": UUID("4c137f6d-bfc7-4e95-b5f1-94eb7c7c0000"),
            "userId": UUID("6dead3f8-599e-11eb-ae93-0242ac130002"),
            "discountId": UUID("5ff9732f-8187-4b14-92d7-89bd72cd91ba"),
            "orderDateTime": new Date("2021-01-17T13:56:45Z"),
            "info": "some comment"
        },
        {
            "_id": UUID("a24e4271-ccbc-4955-a667-028ad37db84e"),
            "userId": UUID("6dead3f8-599e-11eb-ae93-0242ac130002"),
            "discountId": UUID("06734050-5268-4540-91d0-8a4e9a7af6a0"),
            "orderDateTime": new Date("2021-01-17T13:56:45Z"),
            "info": "some comment"
        }
    ];

    let dbName = 'ExadelHEH';

    fillCollections();

    function fillCollections() {

        db = db.getSiblingDB(dbName);

        fillCollection("user", userData);
        fillCollection("history", historyData);
        fillCollection("vendor", vendorData);
        fillCollection("discount", discountData);
        fillCollection("category", categoryData);
        fillCollection("tag", tagData);
        fillCollection("preOrder", preOrderData);
    }

    function fillCollection(collectionName, data) {
        var collection = db.getCollection(collectionName);
        if (collection.count({}) === 0) {
            collection.insertMany(data);
            print("Collection "+ collectionName + " created and filled with initial data");
        } else {
            print("Collection " + collectionName + " already exists");
        }
    }
}