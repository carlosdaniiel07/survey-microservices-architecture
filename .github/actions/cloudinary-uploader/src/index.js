const fs = require("node:fs");
const core = require("@actions/core");

try {
  const assetsPath = core.getInput("assets-path");
  const isValidPath = assetsPath?.trim().length > 0;

  if (!isValidPath) {
    throw new Error(`Invalid path: ${assetsPath}`);
  }

  core.info(`Starting process using directory ${assetsPath}`);

  const files = fs.readdirSync(assetsPath);
  const hasFiles = files.length > 0;

  if (!hasFiles) {
    core.info('There is no files on specified directory')
    return;
  }

  core.info(`${files.length} file(s) found on directory`)
} catch (err) {
  core.setFailed(err.message);
}
