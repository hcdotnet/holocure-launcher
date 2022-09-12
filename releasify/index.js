import kleur from "kleur";
import path from "path";
import prompts from "prompts";
import { dotnet, execFile } from "./cp.js";

function init(x, y, z = "38;5;") {
  let rgx = new RegExp(`\\x1b\\[${y}m`, "g");
  let open = `\x1b[${z}${x}m`,
    close = `\x1b[${y}m`;

  return function (txt) {
    if (/*!$.enabled ||*/ txt == null) return txt;
    return (
      open +
      (!!~("" + txt).indexOf(close) ? txt.replace(rgx, close + open) : txt) +
      close
    );
  };
}

const pinkish = init(219, 39);
const brightWhite = init(15, 39);

function log(message) {
  console.log(kleur.cyan("▶  ") + message);
}

log(
  `Welcome to the ${kleur.cyan("Holo") + pinkish("Cure")}.${brightWhite(
    "Launcher"
  )} releasify tool.`
);

(async () => {
  const resp = await prompts([
    {
      type: "text",
      name: "configuration",
      message: " Enter the build configuration",
    },
    {
      type: "text",
      name: "version",
      message: " Enter the SemVer-compliant version to releasify",
    },
  ]);

  log(`Building main project under configuration "${resp.configuration}"...`);
  await dotnet(["build", path.join("..", "src"), "-c", resp.configuration]);

  log(`Releasifying main project using expected version "${resp.version}"...`);
  const buildPath = path.join(
    "..",
    "src",
    "HoloCure.Launcher.Desktop",
    "bin",
    resp.configuration
  );

  await execFile()("squirrel", [
    "releasify",
    "-p",
    path.join(buildPath, `HoloCure.Launcher.${resp.version}.nupkg`),
    "-f",
    "net6.0",
    "-i",
    path.join("..", "assets", "logo_rocket.ico"),
    "-r",
    path.join("..", "Releases"),
  ]);
})();
