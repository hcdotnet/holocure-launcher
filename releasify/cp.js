import util from "util";
import child_process from "child_process";

const _execFile = util.promisify(child_process.execFile);
export function execFile() {
  return _execFile;
}

export async function dotnet(args) {
  const { stdout, stderr } = await execFile()("dotnet", args);
  if (stderr) throw stderr;
  return stdout;
}
