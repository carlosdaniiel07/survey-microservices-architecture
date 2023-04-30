/** @type {import('next').NextConfig} */

const shouldUseCdn = process.env.USE_CDN || process.env.USE_CDN === "true";
const nextConfig = {
  reactStrictMode: true,
  output: "export",
  assetPrefix: shouldUseCdn
    ? "https://res.cloudinary.com/dxqtcal57/raw/upload"
    : undefined,
};

module.exports = nextConfig;
