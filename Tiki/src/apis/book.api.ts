import {
  CATALOG_PREFIX,
  SUFFIX_LANG_CODES,
  URL_BOOKS,
  URL_GENRES,
} from "@/constants/endpoint";
import { BookDetailDTO } from "@/types/DTOs/BookCatalog/BookDetailDTO.type";
import { CreateBookDTO } from "@/types/DTOs/BookCatalog/CreateBookDTO.type";
import Book from "@/types/Book.type";
import { PaginatedResponse } from "@/types/PaginatedResponse.type";
import http from "@/utils/http";
import Genre from "@/types/Genre.type";

export const bookApi = {
  getBook(id: string) {
    return http.get<BookDetailDTO>(`${CATALOG_PREFIX}${URL_BOOKS}/${id}`);
  },
  getBookByPage(pageIndex: number, pageSize: number) {
    return http.get<PaginatedResponse<BookGeneralInfoDTO>>(
      `${CATALOG_PREFIX}${URL_BOOKS}?pageIndex=${pageIndex}&pageSize=${pageSize}`
    );
  },
  getSearchBookByPage(searchWord: string, pageIndex: number, pageSize: number, isPriceAscend: boolean, genreIds: number[], startPrice: number, endPrice: number) {
    let route = `${CATALOG_PREFIX}${URL_BOOKS}/search?searchWord=${searchWord}&pageIndex=${pageIndex}&pageSize=${pageSize}&isPriceAscend=${isPriceAscend}&startPrice=${startPrice}&endPrice=${endPrice}`;
    
    genreIds.forEach((genre) => {
      route += `&GenreIds=${genre}`;
    });

    return http.get<PaginatedResponse<BookGeneralInfoDTO>>(route);
  },

  getFilterBookByPage(pageIndex: number, pageSize: number, genreIds: number[], authorName: string, startPrice: number, endPrice: number) {
    let route = `${CATALOG_PREFIX}${URL_BOOKS}/filter?pageIndex=${pageIndex}&pageSize=${pageSize}`;
    
    if (authorName !== "" && authorName !== undefined) route += `&authorName=${authorName}`

    if (startPrice >= 0 && endPrice > 0){
      route += `&startPrice=${startPrice}&endPrice=${endPrice}`
    }

    genreIds.forEach((genre) => {
      route += `&GenreId=${genre}`;
    });

    return http.get<PaginatedResponse<BookGeneralInfoDTO>>(route);
  },

  createBook(body: CreateBookDTO) {
    return http.post<Book>(`${CATALOG_PREFIX}${URL_BOOKS}`, body);
  },
  updateBook(body: BookDetailDTO) {
    console.log("Body update: ");
    console.log(body);
    return http.patch<Book>(`${CATALOG_PREFIX}${URL_BOOKS}`, body);
  },
  deleteBook(id: number) {
    return http.delete<string>(`${CATALOG_PREFIX}${URL_BOOKS}?id=${id}`);
  },
  getAllLanguageCodes() {
    return http.get<string[]>(
      `${CATALOG_PREFIX}${URL_BOOKS}${SUFFIX_LANG_CODES}`
    );
  },
  getGenresByPage(pageIndex: number, pageSize: number) {
    return http.get<PaginatedResponse<Genre>>(
      `${CATALOG_PREFIX}${URL_GENRES}?pageIndex=${pageIndex}&pageSize=${pageSize}`
    );
  },
};