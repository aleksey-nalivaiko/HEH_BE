{
    load("userData.js");
    load("historyData.js");
    load("vendorData.js");
    load("discountData.js");
    load("categoryData.js");
    load("tagData.js");
    load("preOrderData.js");

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
            print("Collection " + collectionName + " created and filled with initial data");
        } else {
            print("Collection " + collectionName + " already exists");
        }
    }
}