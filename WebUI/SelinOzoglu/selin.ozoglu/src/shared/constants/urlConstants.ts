// const PHOTO_STOCK_API_BASE_URL="https://localhost:5012/api";
// const WORK_API_BASE_URL="https://localhost:5011";
// const PHOTO_STOCK_API_PHOTOS_FILE_URL="https://localhost:5012/photos/";
// const MAIL_SENDER_API_BASE_URL="https://localhost:5013";
// const IDENTITY4_SERVER_BASE_URL="http://localhost:5001";

// const PHOTO_STOCK_API_BASE_URL="http://gateway.api/services/PhotoStock";
// const WORK_API_BASE_URL="http://gateway.api/services/workitems";
// const PHOTO_STOCK_API_PHOTOS_FILE_URL="http://gateway.api/services/PhotoStock/photos";
// const MAIL_SENDER_API_BASE_URL="http://gateway.api/services/MailSender";
// const IDENTITY4_SERVER_BASE_URL="http://identityserver.api";

// const PHOTO_STOCK_API_BASE_URL="http://localhost:5012/api";
// const WORK_API_BASE_URL="http://localhost:5014";
// const PHOTO_STOCK_API_PHOTOS_FILE_URL="http://localhost:5012/photos/";
// const MAIL_SENDER_API_BASE_URL="http://localhost:5011";
// const IDENTITY4_SERVER_BASE_URL="http://localhost:5001";

//local
// const PHOTO_STOCK_API_BASE_URL="http://localhost:5000/services/PhotoStock";
// const WORK_API_BASE_URL="http://localhost:5000/services/workitems";
// const SETTING_API_BASE_URL="http://localhost:5000/services/Settings";
// const PHOTO_STOCK_API_PHOTOS_FILE_URL="http://localhost:5012/photos/";
// const PHOTO_STOCK_API_SVG_FILE_URL="http://localhost:5012/svg/";
// const MAIL_SENDER_API_BASE_URL="http://localhost:5000/services/MailSender";
// const IDENTITY4_SERVER_BASE_URL="http://localhost:5001";


//localdocker

// const PHOTO_STOCK_API_BASE_URL="https://localhost:19050/services/PhotoStock";
// const WORK_API_BASE_URL="https://localhost:19050/services/workitems";
// const SETTING_API_BASE_URL="https://localhost:19050/services/Settings";
// const PHOTO_STOCK_API_PHOTOS_FILE_URL="https://localhost:19055/photos/";
// const PHOTO_STOCK_API_SVG_FILE_URL="https://localhost:19055/svg/";
// const MAIL_SENDER_API_BASE_URL="https://localhost:19050/services/MailSender";
// const IDENTITY4_SERVER_BASE_URL="https://localhost:19051";


// deploy docker (prod, master) -- cutover'da tekrar aktif edilecek
// const PHOTO_STOCK_API_BASE_URL="https://gateway.selinozoglu.com/services/PhotoStock";
// const WORK_API_BASE_URL="https://gateway.selinozoglu.com/services/workitems";
// const SETTING_API_BASE_URL="https://gateway.selinozoglu.com/services/Settings";
// const PHOTO_STOCK_API_PHOTOS_FILE_URL="https://photostock.selinozoglu.com/photos/";
// const PHOTO_STOCK_API_SVG_FILE_URL="https://photostock.selinozoglu.com/svg/";
// const MAIL_SENDER_API_BASE_URL="https://gateway.selinozoglu.com/services/MailSender";
// const IDENTITY4_SERVER_BASE_URL="https://identity.selinozoglu.com";

// GECICI: feature/monolith-migration paralel test stack (tek host, tum servisler
// portfolio.api uzerinde). Master'a merge etmeden once yukaridaki prod bloguna
// geri donulmeli (tek prod subdomain'e -- ornegin api.selinozoglu.com -- gecilerek).
const PHOTO_STOCK_API_BASE_URL="http://107.174.96.11:8100/services/PhotoStock";
const WORK_API_BASE_URL="http://107.174.96.11:8100/services/workitems";
const SETTING_API_BASE_URL="http://107.174.96.11:8100/services/Settings";
const PHOTO_STOCK_API_PHOTOS_FILE_URL="http://107.174.96.11:8100/photos/";
const PHOTO_STOCK_API_SVG_FILE_URL="http://107.174.96.11:8100/svg/";
const MAIL_SENDER_API_BASE_URL="http://107.174.96.11:8100/services/MailSender";
const IDENTITY4_SERVER_BASE_URL="http://107.174.96.11:8100";

export {
    PHOTO_STOCK_API_BASE_URL,
    WORK_API_BASE_URL,
    PHOTO_STOCK_API_PHOTOS_FILE_URL,
    MAIL_SENDER_API_BASE_URL,
    IDENTITY4_SERVER_BASE_URL,
    SETTING_API_BASE_URL,
    PHOTO_STOCK_API_SVG_FILE_URL
}
