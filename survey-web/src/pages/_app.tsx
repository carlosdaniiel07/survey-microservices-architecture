import { ToastContainer } from "react-toastify";
import "../styles/globals.css";
import type { AppProps } from "next/app";

import "react-toastify/dist/ReactToastify.css";

export default function App({ Component, pageProps }: AppProps) {
  return (
    <>
      <Component {...pageProps} />
      <ToastContainer
        position="bottom-center"
        autoClose={5000}
        hideProgressBar={false}
        closeOnClick
        pauseOnHover
        newestOnTop
        draggable={false}
      />
    </>
  );
}