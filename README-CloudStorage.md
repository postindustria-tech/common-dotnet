# FiftyOne.Common.CloudStorage

![51Degrees](https://raw.githubusercontent.com/51Degrees/common-ci/main/images/logo/360x67.png "Data rewards the curious") **Common Dotnet / CloudStorage**

[![NuGet](https://img.shields.io/nuget/v/FiftyOne.Common.CloudStorage.svg)](https://www.nuget.org/packages/FiftyOne.Common.CloudStorage)

## Features

### Unified interface to interact with blob-like storages

- [[IBlobClient](FiftyOne.Common.CloudStorage/Concepts/IBlobClient.cs)] abstracts away the operations.
  - `WriteAsync` and `DeleteAsync` are self-explanatory.
  - `EffectiveAccountName` and `EffectiveContainerName` properties expose relevant info for analytics and logging purposes.
- [[IBlobData](FiftyOne.Common.CloudStorage/Concepts/IBlobData.cs)] provided by `IBlobClient.GetStreamAsync` abstracts away resources needed to perform download of specific blob.
- [[IBlobMetadata](FiftyOne.Common.CloudStorage/Concepts/IBlobMetadata.cs)] provided by `IBlobClient.GetBlobs` abstracts away resources needed to query blob metadata (e.g. name).
- [[IBlobUploader](FiftyOne.Common.CloudStorage/Concepts/IBlobUploader.cs)] provided by extension (see [[BlobClientExtensions](FiftyOne.Common.CloudStorage/Extensions/BlobClientExtensions.cs)]) method `GetWritableStream` abstracts away writing to a specific blob.

### Unified format of settings from which specific implementations would be fabricated

For compatibility with Azure, `ConnectionString` must be a semicolon-separated array of equals-separated key-value pairs.

Azurite settings:

```cs
"ContainerName=unit-tests;DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;"
```

S3 Ninja settings:

```cs
"S3AccessKey=AKIAIOSFODNN7EXAMPLE;S3SecretKey=wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY;S3BucketName=unit-tests;S3Endpoint=http://localhost:9444/;S3BaseUrl=http://localhost:9444/{bucket}/{key};S3UseSSL=true;S3Region=s3"
```

[[BlobClientFactory](FiftyOne.Common.CloudStorage/Factory/BlobClientFactory.cs)] provides a way to convert such a "packed connection string" into light-weight [[IBlobClientBuilder](FiftyOne.Common.CloudStorage/Factory/IBlobClientBuilder.cs)] instances that can be used to get a specific [[IBlobClient](FiftyOne.Common.CloudStorage/Concepts/IBlobClient.cs)].

### Reusable config section with individually-settable properties

[[CloudStorageConnectionOptions](FiftyOne.Common.CloudStorage/Config/CloudStorageConnectionOptions.cs)] represents a superset of properties usable for any (at least one) [[IBlobClientBuilder](FiftyOne.Common.CloudStorage/Factory/IBlobClientBuilder.cs)].

Currently they are specifically:

```json
{
    // Azure
    "ConnectionString": "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;",
    "ContainerName": "unit-tests"

    ,// -- OR --

    // S3
    "S3AccessKey": "AKIAIOSFODNN7EXAMPLE",
    "S3SecretKey": "wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY",
    "S3BucketName": "data",
    "S3UseSSL": true,
    "S3Region": "s3",
    // + S3-compatible (overrides)
    "S3Endpoint": "http://localhost:9444/",
    "S3BaseUrl": "http://localhost:9444/{bucket}/{key}"
}
```

When [[ICloudStorageConnective](FiftyOne.Common.CloudStorage/Config/ICloudStorageConnective.cs)] "trait" is applied to a parent section -- _(that embeds [[CloudStorageConnectionOptions](FiftyOne.Common.CloudStorage/Config/CloudStorageConnectionOptions.cs)])_ -- [[CloudStorageConnectiveExtensions](FiftyOne.Common.CloudStorage/Config/CloudStorageConnectiveExtensions.cs)] adds corresponding property-setting builder methods.

`PackedConnectionString` property can be used to combine set properties into "packed connection string" usable by [[BlobClientFactory](FiftyOne.Common.CloudStorage/Factory/BlobClientFactory.cs)].

Technically, `ConnectionString` property may already be pre-packed instead of passing other properties individually, e.g.

```json
{
    "ConnectionString": "S3AccessKey=AKIAIOSFODNN7EXAMPLE;S3SecretKey=wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY;S3BucketName=unit-tests;S3Endpoint=http://localhost:9444/;S3BaseUrl=http://localhost:9444/{bucket}/{key};S3UseSSL=true;S3Region=s3"
}
```

## Usage

See [[BlobClientsTest](FiftyOne.Common.CloudStorageTests/BlobClientsTest.cs)] for the usage example.

## Contributing

If you would like to contribuite please follow the contribution guidelines outlined in [CONTRIBUTING.md](https://github.com/51Degrees/common-dotnet/blob/main/CONTRIBUTING.md).
