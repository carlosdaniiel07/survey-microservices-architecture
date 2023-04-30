const fs = require("node:fs");
const path = require("node:path");
const core = require("@actions/core");
const cloudinary = require("cloudinary").v2;

const getFilesFromPath = (value, files = []) => {
  const entries = fs.readdirSync(value);

  entries.forEach((entry) => {
    const fullName = path.join(value, entry);
    const isDir = fs.lstatSync(fullName).isDirectory();

    if (isDir) {
      getFilesFromPath(fullName, files);
    } else {
      files.push(fullName);
    }
  });

  return files;
};

const run = async () => {
  const assetsPath = core.getInput("assets-path");
  const isValidPath = assetsPath?.trim().length > 0;

  if (!isValidPath) {
    throw new Error(`Invalid path: ${assetsPath}`);
  }

  core.info(`Starting process using directory ${assetsPath}`);

  const files = getFilesFromPath(assetsPath);
  const hasFiles = files.length > 0;

  if (!hasFiles) {
    core.info("There is no files on specified directory");
    return;
  }

  core.info(`${files.length} file(s) found on directory`);

  cloudinary.config({
    cloud_name: core.getInput("cloud-name"),
    api_key: core.getInput("api-key"),
    api_secret: core.getInput("api-secret"),
  });

  for (const file of files) {
    const fileNameSplit = String(file).split(path.sep);
    const fileName = fileNameSplit[fileNameSplit.length - 1];
    const folderName = file
      .replace(assetsPath, "")
      .replace(fileName, "")
      .replaceAll(path.sep, "/");

    try {
      await cloudinary.uploader.upload(file, {
        public_id: fileName,
        resource_type: "auto",
        folder: `_next/static/${folderName}`,
        overwrite: true,
      });
    } catch (err) {
      throw new Error(
        `Error while uploading file ${fileName} to Cloudinary: ${err?.message}`
      );
    }
  }
};

run().catch((err) => core.setFailed(err?.message));
