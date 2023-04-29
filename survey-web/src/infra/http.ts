import axios from "axios";

const axiosInstance = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL,
});

export type RequestOptions = {
  params?: Map<string, string>;
  headers?: Record<string, string | number | boolean>;
};

export const get = async <T>(
  url: string,
  options?: RequestOptions
): Promise<T> => {
  const response = await axiosInstance.get<T>(url, {
    params: Object.fromEntries(options?.params ?? []),
    headers: options?.headers,
  });
  return response.data;
};

export const post = async <T>(
  url: string,
  data?: T,
  options?: RequestOptions
): Promise<void> => {
  await axiosInstance.post<T, void>(url, data, {
    params: Object.fromEntries(options?.params ?? []),
    headers: options?.headers,
  });
};

const http = {
  get,
  post,
};

export default http;
