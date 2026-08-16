// Local dev (ng serve). Prod build (ng build --prod) bu dosya yerine
// urlConstants.prod.ts kullanir (bkz. angular.json fileReplacements).
//
// Portfolio.Api "dotnet run" ile sabit portta (http://localhost:5000) calisir.
// Foto/svg DOSYA URL'leri kasitli olarak paralel test stack'ine
// (107.174.96.11:8100) isaret ediyor -- gercek gorseller orada; API cagrilari
// (yukleme/silme dahil) ve token hep local'de kaliyor cunku token, local'in
// imzalama anahtariyla uretiliyor ve uzak sunucudaki API bu anahtari tanimaz.
const PHOTO_STOCK_API_BASE_URL="http://localhost:5000/services/PhotoStock";
const WORK_API_BASE_URL="http://localhost:5000/services/workitems";
const SETTING_API_BASE_URL="http://localhost:5000/services/Settings";
const PHOTO_STOCK_API_PHOTOS_FILE_URL="http://107.174.96.11:8100/photos/";
const PHOTO_STOCK_API_SVG_FILE_URL="http://107.174.96.11:8100/svg/";
const MAIL_SENDER_API_BASE_URL="http://localhost:5000/services/MailSender";
const IDENTITY4_SERVER_BASE_URL="http://localhost:5000";

export {
    PHOTO_STOCK_API_BASE_URL,
    WORK_API_BASE_URL,
    PHOTO_STOCK_API_PHOTOS_FILE_URL,
    MAIL_SENDER_API_BASE_URL,
    IDENTITY4_SERVER_BASE_URL,
    SETTING_API_BASE_URL,
    PHOTO_STOCK_API_SVG_FILE_URL
}
