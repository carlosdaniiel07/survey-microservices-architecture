/** @type {import('next').NextConfig} */

const isProd = process.env.NODE_ENV === "production";
const nextConfig = {
  reactStrictMode: true,
  output: "export",
  assetPrefix: isProd
    ? "https://res.cloudinary.com/dxqtcal57/raw/upload"
    : undefined,
};

module.exports = nextConfig;
