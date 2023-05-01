import { ToastContainer } from "react-toastify";
import "../styles/globals.css";
import type { AppProps } from "next/app";

import "react-toastify/dist/ReactToastify.css";
import Head from "next/head";

export default function App({ Component, pageProps }: AppProps) {
  return (
    <>
      <Head>
        <meta name="viewport" content="width=device-width, initial-scale=1" />
      </Head>
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
