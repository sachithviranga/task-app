import * as CryptoJS from 'crypto-js';

const SECRET_KEY = '🔐SuperSecureKey123'; // For dev only. Use env vars in prod.

export class CryptoUtils {
  static encrypt(value: string): string {
    return CryptoJS.AES.encrypt(value, SECRET_KEY).toString();
  }

  static decrypt(cipherText: string): string {
    const bytes = CryptoJS.AES.decrypt(cipherText, SECRET_KEY);
    return bytes.toString(CryptoJS.enc.Utf8);
  }
}
