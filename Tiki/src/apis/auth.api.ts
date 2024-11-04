import {
  IDENTITY_PREFIX,
  URL_LOGIN,
  URL_LOGOUT,
  URL_PROFILE,
  URL_REGISTER,
  URL_TOKEN,
  URL_UPDATE_PROFILE,
} from "@/constants/endpoint";
import { AuthResponse } from "@/types/AuthResponse.type";
import { PaginatedResponse } from "@/types/PaginatedResponse.type";
import http from "@/utils/http";

export const authApi = {
  register(body: {
    email: string;
    password: string;
    confirmPassword: string;
    username: string;
  }) {
    return http.post(`${IDENTITY_PREFIX}${URL_REGISTER}`, {
      ...body,
      passwordConfirm: body.confirmPassword,
    });
  },
  login(body: { username: string; password: string }) {
    return http.post<AuthResponse>(`${IDENTITY_PREFIX}${URL_LOGIN}`, body);
  },
  logout() {
    return http.post<{
      status: string;
    }>(`${IDENTITY_PREFIX}${URL_LOGOUT}`);
  },
  getResetPassToken(email: string) {
    return http.post<string>(`${IDENTITY_PREFIX}${URL_TOKEN}`, email);
  },
  getUserProfile(userId: string) {
    console.log(`${IDENTITY_PREFIX}${URL_PROFILE}/${userId}`);
    return http.get<User>(`${IDENTITY_PREFIX}${URL_PROFILE}/${userId}`);
  },
  getCustomers(searchTerm: string, pageIndex: number, pageSize: number) {
    return http.get<PaginatedResponse<User>>(
      `${IDENTITY_PREFIX}${URL_UPDATE_PROFILE}?pageIndex=${pageIndex}&pageSize=${pageSize}&searchTerm=${searchTerm}`
    );
  },
};

export default authApi;
