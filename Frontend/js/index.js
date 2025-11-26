const select1 = document.getElementById("root-languaje");
const select2 = document.getElementById("translate-languaje");
const origin = document.getElementById("origin-text");
const target = document.getElementById("target-text");
const swapButton = document.getElementById("button-swap");

async function translateText() {
    const text = origin.value.trim();
    const source = select1.value;
    const targetLang = select2.value;

    if (!text || !source || !targetLang) {
        target.value = "";
        return;
    }

    try {
        const response = await fetch("http://localhost:5170/api", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                textoOrigen: text,
                idiomaOrigen: source,
                idiomaDestino: targetLang
                
            })
        });

        const data = await response.json();
        target.value = data.message || "";

    } catch (error) {
        console.error("Error al traducir:", error);
        target.value = "Error en la traducción";
    }
}

function swapLanguages() {
    const tempLang = select1.value;
    const tempText = origin.value;

    select1.value = select2.value;
    select2.value = tempLang;

    origin.value = target.value;
    target.value = tempText;

    translateText();
}

origin.addEventListener("input", translateText);
select1.addEventListener("change", translateText);
select2.addEventListener("change", translateText);
swapButton.addEventListener("click", swapLanguages);

