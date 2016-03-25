## [1.3.1] - 2016-3-10
### Changed
- Google Play - Google's auto generated IInAppBillingService types have been moved to a separate Android archive; GoogleAIDL. If other plugins define IInAppBillingService, generating duplicate class errors when building for Android, you can delete this AAR to resolve them.

## [1.3.0] - 2016-3-3
### Added
- Receipt validation & parsing library for Google Play and Apple stores. Preliminary documentation can be found [here](https://docs.google.com/document/d/1dJzeoGPeUIUetvFCulsvRz1TwRNOcJzwTDVf23gk8Rg/edit#)

## [1.2.4] - 2016-2-26
### Fixed
- Demo scene error when running on IL2CPP.
- Fixed Use of app_name in Google Play Android manifest causing build errors when exported to Android studio.

## [1.2.3] - 2016-2-11
### Added
- iOS, Mac & Google Play - support for fetching products incrementally with the IStoreController.FetchAdditionalProducts() method that is new to Unity 5.4. Note you will need to be running Unity 5.4 to use this functionality.

## [1.2.2] - 2016-2-9
### Fixed
- Setting IMicrosoftConfiguration.useMockBillingSystem not correctly toggling the local Microsoft IAP simulator.
- Deprecated WinRT.Name and WindowsPhone8.Name; WindowsStore.Name should be used instead for Universal Windows Platform 8.1/10 builds.
- Unnecessary icons and string resources removed from Android archives.

## [1.2.1] - 2016-1-26
### Fixed
- IAP Demo scene not registering its deferred purchase listener.

## [1.2.0] - 2016-1-15
### Added
- tvOS Support. tvOS behaves identically to the iOS and Mac App Stores and shares IAPs with iOS; any IAPs defined for an iOS App will also work when the app is deployed on tvOS.
- Apple Platforms - a method to check whether payment restrictions are in place; [SKPaymentQueue canMakePayments].

```csharp
var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
// Detect if IAPs are enabled in device settings on Apple platforms (iOS, Mac App Store & tvOS).
// On all other platforms this will always return 'true'.
bool canMakePayments = builder.Configure<IAppleConfiguration> ().canMakePayments;
```

### Changed
- Price of fake Editor IAPs from $123.45 to $0.01.

## [1.1.1] - 2016-1-7
### Fixed
- iOS & Mac App Store - Clean up global namespace avoiding symbol conflicts (e.g `Log`)
- Google Play - Activity lingering on the stack when attempting to purchase an already owned non-consumable (Application appeared frozen until back was pressed).
- 'New Game Object' being created by IAP; now hidden in hierarchy and inspector views.

## [1.1.0] - 2015-12-4
### Fixed
- Mac App Store - Base64 receipt payload containing newlines.
- Hiding of internal store implementation classes not necessary for public use.

### Added
- IAppleConfiguration featuring an 'appReceipt' string property for reading the App Receipt from the device, if any;

```csharp
var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
// On iOS & Mac App Store, receipt will be a Base64 encoded App Receipt, or null
// if no receipt is present on the device.
// On other platforms, receipt will be a dummy placeholder string.
string receipt = builder.Configure<IAppleConfiguration> ().appReceipt;
```

## [1.0.2] - 2015-11-6
### Added
- Demo scene uses new GUI (UnityEngine.UI).
- Fake IAP confirmation dialog when running in the Editor to allow you to test failed purchases and initialization failures.

## [1.0.1] - 2015-10-21
### Fixed
- Google Play: Application IStoreListener methods executing on non scripting thread.
- Apple Stores: NullReferenceException when a user owns a product that was not requested by the Application during initialization.
- Tizen, WebGL, Samsung TV: compilation errors when building a project that uses Unity IAP.

## [1.0.0] - 2015-10-01
### Added
- Google Play
- Apple App Store
- Mac App Store
- Windows Store (Universal)
