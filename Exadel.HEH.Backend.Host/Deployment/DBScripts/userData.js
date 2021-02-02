let userData = [
  {
    "_id": UUID("6dead3f8-599e-11eb-ae93-0242ac130002"),
    "role": "Moderator",
    "name": "Mary Jones",
    "email": "maryj@exadel.com",
    "password": "9DLxQwsPzPOMkPf1dTDbSs1UCMMXy67LWcdN742oiYU=",
    "salt": "AbPmq+l8h0GXeDxaLsLwr8aB2JDJ6vi1wLMnPRRrBBL6Tf01UBF6+B+svtaLYltS76sUKiTCiY48N1Sx70Jg8A==",
    "address":
    {
        "_id": UUID("03581ffe-5d6e-11eb-ae93-0242ac130002"),
        "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
        "cityId": UUID("3075aada-5efc-11eb-ae93-0242ac130002"),
        "street": "street 1"
    },
    "isActive": true,
    "categoryNotifications": [
      UUID("fb42cd13-1c15-4388-bb3d-216376b5a225"),
      UUID("a3ab67b9-1a72-48e1-a928-e0dbe0b17a5c"),
      UUID("c86bec9f-2ef9-4be2-bd21-af14aa0d187f")
    ],
    "tagNotifications": [
      UUID("ab4b5672-e2f6-4935-ba03-58851d9c5991"),
      UUID("22427389-7127-41b4-9aa4-af2078980cf8")
    ],
    "vendorNotifications": [
      UUID("662c27f1-8f63-44ac-a873-aac6f09ab173")
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
    "role": "Administrator",
    "name": "Jack Smith",
    "email": "jacks@exadel.com",
    "password": "J1o+bBVlrnls2e2YTUAOq75ZKwUJBiIyc4bd8o8RJ9E=",
    "salt": "wmns4m7RG0Cw7JplXCKrSJStHydp2vqmIVQEt/6EgUGNUyfPPI21OSJl+0C1caQPA65QZotTywR0mUt+ZPC/7Q==",
    "address":
    {
        "_id": UUID("194449b4-5d6e-11eb-ae93-0242ac130002"),
        "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
        "cityId": UUID("3075aada-5efc-11eb-ae93-0242ac130002"),
        "street": "street 2"
    },
    "isActive": true,
    "categoryNotifications": [
      UUID("a3ab67b9-1a72-48e1-a928-e0dbe0b17a5c"),
      UUID("c86bec9f-2ef9-4be2-bd21-af14aa0d187f")
    ],
    "tagNotifications": [
      UUID("81cf086a-84a8-4291-9a55-791f1005f09e"),
      UUID("ab4b5672-e2f6-4935-ba03-58851d9c5991")
    ],
    "vendorNotifications": [
      UUID("ef809bbf-1ff7-4f1c-ac2b-ab5040294cf1")
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
    "role": "Employee",
    "name": "Alex Rollings",
    "email": "alexr@exadel.com",
    "password": "mLt6OCqm/0ZqUCCcH3cq8PtNllmD/FIfj2KW+OBMmh0=",
    "salt": "F150N1dRfQTMD4ZzG9yUP7vmoMKm5CSo0/h3q2822NaA6/KI+L8VhrOWktYyNuI6oi2t0uQYw3tgKS0ZxItXFg==",
    "address":
    {
        "_id": UUID("22d45b7c-5d6e-11eb-ae93-0242ac130002"),
        "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
        "cityId": UUID("3075aada-5efc-11eb-ae93-0242ac130002"),
        "street": "street 3"
    },
    "isActive": true,
    "categoryNotifications": [
      UUID("a3ab67b9-1a72-48e1-a928-e0dbe0b17a5c"),
      UUID("c86bec9f-2ef9-4be2-bd21-af14aa0d187f")
    ],
    "tagNotifications": [
      UUID("81cf086a-84a8-4291-9a55-791f1005f09e"),
      UUID("ab4b5672-e2f6-4935-ba03-58851d9c5991")
    ],
    "vendorNotifications": [
      UUID("ef809bbf-1ff7-4f1c-ac2b-ab5040294cf1")
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
        "role": "Employee",
        "name": "Amy Tomas",
        "email": "amyt@exadel.com",
        "password": "wVCtF6ViB9aftiVx5lpy3EneokHOAn+9aAbcpQEyoYk=",
        "salt": "GNyRwwotie5swo2qlCwVyH69nB7w3s/zcRLFqWoapeQ8cbzV9YXMqdI20Tcn4PtQK8DlEmBge8NUUo/galOH/Q==",
        "address": {
            "_id": UUID("2b80c468-5d6e-11eb-ae93-0242ac130002"),
            "countryId": UUID("0b343598-5efc-11eb-ae93-0242ac130002"),
            "cityId": UUID("3075aada-5efc-11eb-ae93-0242ac130002"),
            "street": "street 4"
        },
        "isActive": false,
        "categoryNotifications": [
            UUID("a3ab67b9-1a72-48e1-a928-e0dbe0b17a5c"),
            UUID("c86bec9f-2ef9-4be2-bd21-af14aa0d187f")
        ],
        "tagNotifications": [
            UUID("81cf086a-84a8-4291-9a55-791f1005f09e"),
            UUID("ab4b5672-e2f6-4935-ba03-58851d9c5991")
        ],
        "vendorNotifications": [
            UUID("ef809bbf-1ff7-4f1c-ac2b-ab5040294cf1")
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