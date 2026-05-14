
async function __DCLoadScript20251231(bsScriptContent, strFileName, bolTryLoadFile) {
    if (strFileName != null && strFileName.length > 0) {
        if (bsScriptContent == null || bsScriptContent.length == 0 || bolTryLoadFile) {
            var strUrl4 = new URL("_framework/" + strFileName, document.baseURI).href;
            const myResponse = await fetch(strUrl4);
            if (myResponse.ok) {
                var script2 = document.createElement("script");
                script2.src = strUrl4;
                script2.async = false;
                script2.defer = false;
                const p = new Promise((resolve, reject) => {
                    script2.addEventListener("load", () => resolve());
                    script2.addEventListener("error", (e) => reject(new Error("Failed to load script: " + strUrl4)));
                });
                (document.head || document.getElementsByTagName("head")[0]).appendChild(script2);
                await p;
                return;
            }
        }
    }
    // bsScriptContent: Uint8Array containing JS code
    if (!bsScriptContent || !(bsScriptContent instanceof Uint8Array)) {
        throw new Error("__DCLoadScript20251231 expects a Uint8Array from " + strFileName);
    }

    // Create a Blob from the content
    const blob = new Blob([bsScriptContent], { type: "application/javascript" });
    const blobUrl = URL.createObjectURL(blob); // e.g., blob:https://... or blob:http://...

    // Create and append the script element under the document head
    const script = document.createElement("script");
    script.src = blobUrl;
    script.async = false;
    script.defer = false;

    const p2 = new Promise((resolve, reject) => {
        script.addEventListener("load", function () {
            //URL.revokeObjectURL(blobUrl);
            resolve();
        });
        script.addEventListener("error", function (e2) {
            //URL.revokeObjectURL(blobUrl);
            console.log(e2);
            reject(new Error("Failed to load blob script"));
        });
    });
    try {
        (document.head || document.getElementsByTagName("head")[0]).appendChild(script);
    }
    catch (ext) {
        console.log(ext);
        throw ext;
    }
    await p2;
}
