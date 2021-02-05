let tagData = [
    {
        "_id": UUID("d1adffea-945d-44c6-a79d-b935ccec3051"),
        "name": "Pizza",
        "categoryId": UUID("ab067429-6fbf-4798-92bf-d62012db8f25")
    },
    {
        "_id": UUID("1650b9cf-9568-4964-be9c-61b7efb8398e"),
        "name": "Sushi",
        "categoryId": UUID("ab067429-6fbf-4798-92bf-d62012db8f25")
    },
    {
        "_id": UUID("3fdb85d8-33da-4de9-99c6-0ba8c2c00c83"),
        "name": "Doner",
        "categoryId": UUID("ab067429-6fbf-4798-92bf-d62012db8f25")
    },
    {
        "_id": UUID("88a31bf1-2c38-4a62-b500-d6689f92caf1"),
        "name": "FastFood",
        "categoryId": UUID("ab067429-6fbf-4798-92bf-d62012db8f25")
    },
    {
        "_id": UUID("7de99c4f-d795-4a28-a7cf-1e2f952c71ee"),
        "name": "StreetFood",
        "categoryId": UUID("ab067429-6fbf-4798-92bf-d62012db8f25")
    },
    {
        "_id": UUID("7bb365ae-87d7-458d-b170-00ceabea9c54"),
        "name": "Bar",
        "categoryId": UUID("ab067429-6fbf-4798-92bf-d62012db8f25")
    },
    {
        "_id": UUID("5c2d584d-40fd-4ac6-ac39-1cf93c1f498e"),
        "name": "Cakes",
        "categoryId": UUID("ab067429-6fbf-4798-92bf-d62012db8f25")
    },
    {
        "_id": UUID("8d1a2c85-53bb-4504-bb3a-c34b1ba23b7d"),
        "name": "Coffee",
        "categoryId": UUID("ab067429-6fbf-4798-92bf-d62012db8f25")
    },
    {
        "_id": UUID("eabc9d48-d6ba-4af9-a1ac-e14e6ad201c6"),
        "name": "Cafe",
        "categoryId": UUID("ab067429-6fbf-4798-92bf-d62012db8f25")
    },
    {
        "_id": UUID("0cdedb5c-2b90-483c-9931-40abb013f7d2"),
        "name": "Gym",
        "categoryId": UUID("751dc6e4-603b-477f-91dc-97c09d13d96a")
    },
    {
        "_id": UUID("055a0b7f-dfed-4e7a-9ea4-0e380bb6dcfb"),
        "name": "Pool",
        "categoryId": UUID("751dc6e4-603b-477f-91dc-97c09d13d96a")
    },
    {
        "_id": UUID("751ad78c-2286-41ef-af98-5faed3d5035c"),
        "name": "Fitness",
        "categoryId": UUID("751dc6e4-603b-477f-91dc-97c09d13d96a")
    },
    {
        "_id": UUID("a17b37a2-8085-4c62-953e-f7e37116abf0"),
        "name": "Massage",
        "categoryId": UUID("751dc6e4-603b-477f-91dc-97c09d13d96a")
    },
    {
        "_id": UUID("3d9fb1b9-c326-4a85-986f-237cabc0e368"),
        "name": "Dance",
        "categoryId": UUID("751dc6e4-603b-477f-91dc-97c09d13d96a")
    },
    {
        "_id": UUID("45fd014b-1389-4036-824a-7dd11c43d1cd"),
        "name": "Tennis",
        "categoryId": UUID("751dc6e4-603b-477f-91dc-97c09d13d96a")
    },
    {
        "_id": UUID("a79b65d9-5c72-47be-a913-b646261fbfb5"),
        "name": "Manicure",
        "categoryId": UUID("11f8463a-827f-4873-8550-62e0ee03b369")
    },
    {
        "_id": UUID("e27eb33a-8eda-45b3-999b-318902a397b2"),
        "name": "Spa",
        "categoryId": UUID("11f8463a-827f-4873-8550-62e0ee03b369")
    },
    {
        "_id": UUID("6bf3f73e-c844-4cd5-a3bc-6f3364530468"),
        "name": "Haircut",
        "categoryId": UUID("11f8463a-827f-4873-8550-62e0ee03b369")
    },
    {
        "_id": UUID("bfd824c2-0f9d-4b76-848f-37828c161d9e"),
        "name": "Barbershop",
        "categoryId": UUID("11f8463a-827f-4873-8550-62e0ee03b369")
    },
    {
        "_id": UUID("22c2c1b7-a913-4db3-bfd1-e298a9a086f7"),
        "name": "Zoo",
        "categoryId": UUID("8f411964-f934-4302-bdd0-db2fc2a549e7")
    },
    {
        "_id": UUID("95924523-70a8-4378-820a-ec716dc79a30"),
        "name": "Trampoline",
        "categoryId": UUID("8f411964-f934-4302-bdd0-db2fc2a549e7")
    },
    {
        "_id": UUID("babe80d3-5042-43a2-97ce-3f8f542b303a"),
        "name": "Rental",
        "categoryId": UUID("8f411964-f934-4302-bdd0-db2fc2a549e7")
    },
    {
        "_id": UUID("7f7b40be-7c47-4866-a867-b60504c4bb4f"),
        "name": "Bowling",
        "categoryId": UUID("8f411964-f934-4302-bdd0-db2fc2a549e7")
    },
    {
        "_id": UUID("bb5fd66d-63d0-4dde-8580-7a26771bc129"),
        "name": "Quests",
        "categoryId": UUID("8f411964-f934-4302-bdd0-db2fc2a549e7")
    },
    {
        "_id": UUID("ff9ea79a-94ab-47a6-b9ea-1c3aa745b278"),
        "name": "Karting",
        "categoryId": UUID("8f411964-f934-4302-bdd0-db2fc2a549e7")
    },
    {
        "_id": UUID("cd9ab99d-4d26-4611-86df-fa7433d80050"),
        "name": "Fuel",
        "categoryId": UUID("1c22cbf6-9692-4207-83e3-8d51a6bbd8be")
    },
    {
        "_id": UUID("5d358fae-be94-4ed8-8c4e-47d201932f9f"),
        "name": "CarWash",
        "categoryId": UUID("1c22cbf6-9692-4207-83e3-8d51a6bbd8be")
    },
    {
        "_id": UUID("d1575ac0-cedf-49fd-9fb5-18579878720d"),
        "name": "TireFitting",
        "categoryId": UUID("1c22cbf6-9692-4207-83e3-8d51a6bbd8be")
    },
    {
        "_id": UUID("76dce0ac-3d3d-49cd-8f15-fbe43ebe9900"),
        "name": "WheelAlignment",
        "categoryId": UUID("1c22cbf6-9692-4207-83e3-8d51a6bbd8be")
    },
    {
        "_id": UUID("1b4702a7-999a-479d-acd8-97e4563b98dc"),
        "name": "Cleaning",
        "categoryId": UUID("1c22cbf6-9692-4207-83e3-8d51a6bbd8be")
    },
    {
        "_id": UUID("2bc8762b-8790-4dc7-90ef-563a6337f4ca"),
        "name": "Polishing",
        "categoryId": UUID("1c22cbf6-9692-4207-83e3-8d51a6bbd8be")
    },
    {
        "_id": UUID("d364664d-6b92-43e4-883d-5b663adbbf87"),
        "name": "Repair",
        "categoryId": UUID("1c22cbf6-9692-4207-83e3-8d51a6bbd8be")
    },
    {
        "_id": UUID("0998631c-eb37-4767-8ff7-12bc8166dfae"),
        "name": "Hotel",
        "categoryId": UUID("97c8c966-634a-4a3a-b87b-89d2d31587d3")
    },
    {
        "_id": UUID("58e6f3d3-a1c4-488f-96a7-325824993816"),
        "name": "Hostel",
        "categoryId": UUID("97c8c966-634a-4a3a-b87b-89d2d31587d3")
    },
    {
        "_id": UUID("c125a50e-ed05-45b3-a972-48718cdb109f"),
        "name": "Bus tour",
        "categoryId": UUID("97c8c966-634a-4a3a-b87b-89d2d31587d3")
    },
    {
        "_id": UUID("354a39e8-887c-4ac0-b5ec-b7ff4268e321"),
        "name": "Sanatorium",
        "categoryId": UUID("97c8c966-634a-4a3a-b87b-89d2d31587d3")
    },
    {
        "_id": UUID("519df895-7043-4958-b0b5-82dee4c41774"),
        "name": "VisaSupport",
        "categoryId": UUID("97c8c966-634a-4a3a-b87b-89d2d31587d3")
    },
    {
        "_id": UUID("552c6066-4474-4273-8c0c-5832895f34a7"),
        "name": "Airport",
        "categoryId": UUID("97c8c966-634a-4a3a-b87b-89d2d31587d3")
    },
    {
        "_id": UUID("3d9590bb-49b6-4f08-b717-264c34ec6d26"),
        "name": "Music",
        "categoryId": UUID("66490a95-4e19-4cea-9c60-a12031bd330c")
    },
    {
        "_id": UUID("42a69c2c-d3a0-4ec8-b0ef-5319bc27538f"),
        "name": "Drawing",
        "categoryId": UUID("66490a95-4e19-4cea-9c60-a12031bd330c")
    },
    {
        "_id": UUID("8a60ece5-47c4-4648-b564-569c4d5ea830"),
        "name": "Dancing",
        "categoryId": UUID("66490a95-4e19-4cea-9c60-a12031bd330c")
    },
    {
        "_id": UUID("223490e0-01db-4a13-9a67-74f23cc33ad9"),
        "name": "Seminars",
        "categoryId": UUID("66490a95-4e19-4cea-9c60-a12031bd330c")
    },
    {
        "_id": UUID("f96a1ca6-0eed-4697-ac70-d4f2bf1f560e"),
        "name": "Trainings",
        "categoryId": UUID("66490a95-4e19-4cea-9c60-a12031bd330c")
    },
    {
        "_id": UUID("fd827636-d1bf-466e-bb8d-36198e643e1a"),
        "name": "Languages",
        "categoryId": UUID("66490a95-4e19-4cea-9c60-a12031bd330c")
    }
]