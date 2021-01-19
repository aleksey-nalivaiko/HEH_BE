{
	let userData = [{
		"_id": UUID("6dead3f8-599e-11eb-ae93-0242ac130002"),
		"role": "moderator",
		"name": "Mary Jhons",
		"email": "maryj@exadel.com",
		"password": "a5Adj7O7",
		"address":
		{
			country: "Belarus",
			city: "Minsk",
			street: "ul. Naturalistov 3"
		},
		"isActive": true,
		"categoryNotifications": [
			//fill categoryID
		],
		"tagNotifications": [
			//fill tagID
		],
		"vaendorNotifications": [
			//fill vendorID
		],
		"newVendorNotificationIsOn": true,
		"newDiscountNotificationIsOn": true,
		"hotDiscountsNotificationIsOn": false,
		"cityChangeNotificationIsOn": true,
		"favorites": [
			{
				"discountId": "", //fill
				"note": "" //fill
			},
			{
				"discountId": "", //fill
				"note": "" //fill
			},
		]
	},
	{
		"_id": UUID("c8fd9ed0-59a1-11eb-ae93-0242ac130002"),
		"role": "administrator",
		"name": "Jack Smith",
		"email": "jacks@exadel.com",
		"password": "895sdj9O765",
		"address":
		{
			country: "Belarus",
			city: "Minsk",
			street: " ul. Pritytskogo 156"
		},
		"isActive": true,
		"categoryNotifications": [
			//fill categoryID
		],
		"tagNotifications": [
			//fill tagID
		],
		"vaendorNotifications": [
			//fill vendorID
		],
		"newVendorNotificationIsOn": true,
		"newDiscountNotificationIsOn": true,
		"hotDiscountsNotificationIsOn": false,
		"cityChangeNotificationIsOn": true,
		"favorites": [
			{
				"discountId": "", //fill
				"note": "" //fill
			},
			{
				"discountId": "", //fill
				"note": "" //fill
			},
		]
	},
	{
		"_id": UUID("a7df950e-59a2-11eb-ae93-0242ac130002"),
		"role": "employee",
		"name": "Alex Rollings",
		"email": "alexr@exadel.com",
		"password": "5875soj9O765",
		"address": //id?
		{
			country: "Belarus",
			city: "Minsk",
			street: " ul. Pritytskogo 156"
		},
		"isActive": true,
		"categoryNotifications": [
			//fill categoryID
		],
		"tagNotifications": [
			//fill tagID
		],
		"vaendorNotifications": [
			//fill vendorID
		],
		"newVendorNotificationIsOn": true,
		"newDiscountNotificationIsOn": true,
		"hotDiscountsNotificationIsOn": false,
		"cityChangeNotificationIsOn": true,
		"favorites": [
			{
				"discountId": "", //fill
				"note": "" //fill
			},
			{
				"discountId": "", //fill
				"note": "" //fill
			},
		]
	},
	{
		"_id": UUID("662db544-59a4-11eb-ae93-0242ac130002"),
		"role": "employee",
		"name": "Alex Rollings",
		"email": "alexr@exadel.com",
		"password": "5875soj9O765",
		"address":
		{
			country: "Belarus",
			city: "Minsk",
			street: " ul. Pritytskogo 156"
		},
		"isActive": false,
		"categoryNotifications": [
			//fill categoryID
		],
		"tagNotifications": [
			//fill tagID
		],
		"vaendorNotifications": [
			//fill vendorID
		],
		"newVendorNotificationIsOn": true,
		"newDiscountNotificationIsOn": true,
		"hotDiscountsNotificationIsOn": false,
		"cityChangeNotificationIsOn": true,
		"favorites": [
			{
				"discountId": "", //fill
				"note": "" //fill
			},
			{
				"discountId": "", //fill
				"note": "" //fill
			},
		]
	}
	];

	let historyData = [{
		"_id": UUID("209271f8-59b5-11eb-ae93-0242ac130002"),
		"_userId": UUID("6dead3f8-599e-11eb-ae93-0242ac130002"),
		"role": "moderator",
		"name": "Mary Jhons",
		"email": "maryj@exadel.com",
		"action": "add new vendor: Dominos", //change
		"dateTime": new Date("2021-01-18T16:00:00Z")
	},
	{
		"_id": UUID("1c2ec8c8-59b5-11eb-ae93-0242ac130002"),
		"_userId": UUID("6dead3f8-599e-11eb-ae93-0242ac130002"),
		"role": "moderator",
		"name": "Mary Jhons",
		"email": "maryj@exadel.com",
		"action": "add new discount: 10% for second pizza", //change
		"dateTime": new Date("2021-01-18T16:10:12Z")
	},
	{
		"_id": UUID("178a3744-59b5-11eb-ae93-0242ac130002"),
		"_userId": UUID("c8fd9ed0-59a1-11eb-ae93-0242ac130002"),
		"role": "administrator",
		"name": "Jack Smith",
		"email": "jacks@exadel.com",
		"action": "remove discount: 70% for second pizza", //change
		"dateTime": new Date("2021-01-17T13:56:45Z")
	},
	];

	let dbName = 'HEHDB';

	checkDBIsPresent();
	fillCollections();

	function checkDBIsPresent() {
		var dbInd = db.getMongo().getDBNames().indexOf(dbName);

		if (dbInd === -1) {
			print("Db will be created");
		}
		else {
			print("Database " + dbName + " already exists");
		}
	}

	function fillCollections() {

		db = db.getSiblingDB(dbName);

		fillCollection("user", userData);
		fillCollection("history", historyData);
		//fillCollection("vendor", vendorData);
		//fillCollection("discount", discountData);
		//fillCollection("category", categoryData);
		//fillCollection("tag", tagData);
		//fillCollection("preOrder", preOrderData);
	}

	function fillCollection(collectionName, data) {
		var collection = db.getCollection(collectionName);
		if (collection.count({}) === 0) {
			collection.insertMany(data);
			print("Collection " + collectionName + " created and filled with initial data");
		}
		else {
			print("Collection " + collectionName + " already exists");
		}
	}
}