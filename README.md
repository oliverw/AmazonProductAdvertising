# Nager.AmazonProductAdvertising
Allow access to amazon product advertising API (`paapi5`), you can search a product over the name or a keyword.

### Usage

Please check the AmazonEndpoint is correct for your Country.
- Amazon Germany use `AmazonEndpoint.DE`
- Amazon Spain use `AmazonEndpoint.ES`
- Amazon United Kingdom use `AmazonEndpoint.UK`

##### Item Search (simple)
```cs
var signer = new AwsSigner();
var client = new AmazonProductAdvertisingClient(signer,"accesskey", "secretkey",AmazonEndpoint.US, "yourPartnerTag");
var result = await client.SearchItemsAsync("canon eos");
```

##### Item Search (advanced)
```cs
var signer = new AwsSigner();
var client = new AmazonProductAdvertisingClient(signer,"accesskey", "secretkey",AmazonEndpoint.US, "yourPartnerTag");
var searchRequest = new SearchRequest
{
    Keywords = "canon eos",
    ItemPage = 2,
    Resources = new []
    {
        //You can found all available Resources in the documentation
        //https://webservices.amazon.com/paapi5/documentation/search-items.html#resources-parameter
        "Images.Primary.Large",
        "ItemInfo.Title",
        "ItemInfo.Features"
    }
};
var result = await client.SearchItemsAsync(searchRequest);
```

##### Item Lookup
```cs
var signer = new AwsSigner();
var client = new AmazonProductAdvertisingClient(signer,"accesskey", "secretkey",AmazonEndpoint.US, "yourPartnerTag");
var result = await client.GetItemsAsync("B00BYPW00I");
```

##### Multi Item Lookup
```cs
var signer = new AwsSigner();
var client = new AmazonProductAdvertisingClient(signer,"accesskey", "secretkey",AmazonEndpoint.US, "yourPartnerTag");
var result = await client.GetItemsAsync(new string[] { "B00BYPW00I", "B004MKNBJG" });
```

### Amazon Documentation
- [API Reference](https://webservices.amazon.com/paapi5/documentation/)
- [Product Advertising API Scratchpad](https://webservices.amazon.com/paapi5/documentation/play-around-using-scratchpad.html)

