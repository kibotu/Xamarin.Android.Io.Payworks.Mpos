using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Util;

namespace Demo
{
    [Activity(Label = "Demo", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate { Pay(); };
        }

        /// <summary>
        /// http://www.payworks.mpymnt.com/node/143
        /// </summary>
        private void Pay()
        {
            //MposUi ui = MposUi.initialize(this, ProviderMode.MOCK, "merchantIdentifier", "merchantSecretKey");

            //ui.getConfiguration().setSummaryFeatures(EnumSet.of(
            //        // Add this line, if you do want to offer printed receipts
            //        // MposUiConfiguration.SummaryFeature.PRINT_RECEIPT,
            //        MposUiConfiguration.SummaryFeature.SEND_RECEIPT_VIA_EMAIL)
            //);

            //// Start with a mocked card reader:
            //AccessoryParameters accessoryParameters = new AccessoryParameters.Builder(AccessoryFamily.MOCK)
            //        .mocked()
            //        .build();
            //ui.getConfiguration().setTerminalParameters(accessoryParameters);

            //TransactionParameters transactionParameters = new TransactionParameters.Builder()
            //        .charge(new BigDecimal("5.00"), io.mpos.transactions.Currency.EUR)
            //        .subject("Bouquet of Flowers")
            //        .customIdentifier("yourReferenceForTheTransaction")
            //        .build();

            //Intent intent = ui.createTransactionIntent(transactionParameters);
            //startActivityForResult(intent, MposUi.REQUEST_CODE_PAYMENT);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Log.Verbose("Demo", $"[OnActivityResult] {requestCode} {resultCode} {data}");
            //    if (requestCode == MposUi.REQUEST_CODE_PAYMENT)
            //    {
            //        if (resultCode == MposUi.RESULT_CODE_APPROVED)
            //        {
            //            // Transaction was approved
            //            Toast.makeText(this, "Transaction approved", Toast.LENGTH_LONG).show();
            //        }
            //        else
            //        {
            //            // Card was declined, or transaction was aborted, or failed
            //            // (e.g. no internet or accessory not found)
            //            Toast.makeText(this, "Transaction was declined, aborted, or failed", Toast.LENGTH_LONG).show();
            //        }
            //        // Grab the processed transaction in case you need it
            //        // (e.g. the transaction identifier for a refund).
            //        // Keep in mind that the returned transaction might be null
            //        // (e.g. if it could not be registered).
            //        Transaction transaction = MposUi.getInitializedInstance().getTransaction();
            //    }
        }
    }
}