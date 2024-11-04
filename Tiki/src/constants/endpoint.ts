export const URL_BASE = "https://localhost:44395";
// Identity api
export const IDENTITY_PREFIX = "/api";
export const URL_REGISTER = "/auth/register";
export const URL_LOGIN = "/auth/login";
export const URL_LOGOUT = "/auth/logout";
export const URL_TOKEN = "/auth/resetpass-token";
export const URL_RESETPASS = "/auth/resetpass";
export const URL_FILE_UPLOAD = "/files/upload";
export const URL_UPDATEPASS = "/update-password";
export const URL_PROFILE = "/auth/profile";
export const URL_UPDATE_PROFILE = "/auth";

// Catalog api
export const CATALOG_PREFIX = "/api/v1";
export const URL_BOOKS = "/books";
export const URL_GENRES = "/genres";
export const URL_BOOK_REVIEWS = "/book-reviews";
export const URL_FORMATS = "/formats";
export const URL_PUBLISHERS = "/publishers";
export const SUFFIX_LANG_CODES = "/lang-codes";

// Basket api
export const CART_PREFIX = "/basket.api/v1";
export const URL_CART = "/baskets";

// Ordering api
export const ORDERING_PREFIX = "/ordering.api/v1";
export const URL_ORDERS = "/orders";
export const URL_PAYMENT = "/payment-webhook/create-checkout-session";
export const URL_TRANSACTIONS = "/transactions";
export const URL_ADDRESS = "/addresses";
export const URL_PAYMENT_METHOD = "/payment-methods"

// RecSys api
export const RECSYS_PREFIX = "/recsys.api";
export const URL_PREDICT = "/predict";
