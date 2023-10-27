﻿module Tests.SqlParsing

open Xunit
open Tests.TestHelper
open StereoDB.FSharp

let sqlCompilationFailure (db: IStereoDb<Schema>) sql expectedError = 
    try
        db.ExecuteSql sql
        Assert.True(false, "Should not happens")
    with ex ->
        Assert.Equal (expectedError, ex.Message)

[<Fact>]
let ``Fails on not existing table`` () =
    let db = Db.Create()
    
    sqlCompilationFailure db "UPDATE NonExisting SET Quantity = 2" "Table NonExisting is not defined"

[<Fact>]
let ``Fails on not existing column`` () =
    let db = Db.Create()
    
    sqlCompilationFailure db "UPDATE Books SET NonExisting = 2" "Column NonExisting does not exist in table Books"