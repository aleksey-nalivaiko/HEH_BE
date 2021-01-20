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

        fillCollection("User", userData);
        fillCollection("History", historyData);
        fillCollection("Vendor", vendorData);
        fillCollection("Discount", discountData);
        fillCollection("Category", categoryData);
        fillCollection("Tag", tagData);
        fillCollection("PreOrder", preOrderData);
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