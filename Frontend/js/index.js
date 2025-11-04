fetch("http://localhost:5170/Hello/world")
.then(x => x.text())
.then(x => console.log(x))
.catch(error => console.log("error:", error));

document.getElementById("button-swap").onclick = function () {
            const select1 = document.getElementById("root-languaje");
            const select2 = document.getElementById("translate-languaje");

            const origin = document.getElementById("origin-text");
            const target = document.getElementById("target-text");

            const value1 = select1.value;
            const value2 = select2.value;
            const orintext = origin.value;
            const targtext = target.value;
            

            select1.value = value2;
            select2.value = value1;
            origin.value = targtext;
            target.value = orintext;

        }
