// Prod (master, ng build --prod). angular.json fileReplacements ile bu dosya
// urlConstants.ts'in yerine derlenir. Alt domainler ayni kaliyor
// (gateway./identity./photostock.selinozoglu.com) -- sadece nginx tarafinda
// hepsi ayni portfolio.api'ye (5100) yonleniyor, DNS degismedi.
const PHOTO_STOCK_API_BASE_URL="https://gateway.selinozoglu.com/services/PhotoStock";
const WORK_API_BASE_URL="https://gateway.selinozoglu.com/services/workitems";
const SETTING_API_BASE_URL="https://gateway.selinozoglu.com/services/Settings";
const PHOTO_STOCK_API_PHOTOS_FILE_URL="https://photostock.selinozoglu.com/photos/";
const PHOTO_STOCK_API_SVG_FILE_URL="https://photostock.selinozoglu.com/svg/";
const MAIL_SENDER_API_BASE_URL="https://gateway.selinozoglu.com/services/MailSender";
const IDENTITY4_SERVER_BASE_URL="https://identity.selinozoglu.com";

export {
    PHOTO_STOCK_API_BASE_URL,
    WORK_API_BASE_URL,
    PHOTO_STOCK_API_PHOTOS_FILE_URL,
    MAIL_SENDER_API_BASE_URL,
    IDENTITY4_SERVER_BASE_URL,
    SETTING_API_BASE_URL,
    PHOTO_STOCK_API_SVG_FILE_URL
}
