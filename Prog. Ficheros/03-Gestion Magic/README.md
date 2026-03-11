# MTG-Commander-Saving

## 📜 EL ARCHIVO DE COMMANDER (VAULT DE DOMINARIA)

### 📝 1. El Escenario

> *“El verdadero poder no está en los hechizos que conoces, sino en los que sabes encontrar cuando los necesitas.”*

En el plano de Dominaria, los archivistas del multiverso mantienen un enorme registro de cartas del juego Magic: The Gathering.

Este archivo no solo almacena cartas, sino que también registra mazos del formato Commander, utilizados por los magos para poner a prueba su estrategia.

Con el paso del tiempo, el número de cartas y mazos ha crecido demasiado y el archivo necesita un sistema que permita organizar la colección, consultar la información y construir mazos válidos.

Tu tarea consiste en desarrollar una aplicación capaz de gestionar el archivo de cartas y los mazos de Commander creados a partir de ellas.

---

### 🧩 2. El Archivo Arcano (Cartas)

> *“Cada carta encierra un fragmento de poder esperando ser comprendido.”*

Cada carta registrada en el sistema deberá contener la información mínima necesaria para poder identificarse dentro del archivo.

Datos mínimos de una carta:

- Nombre de la carta

- Identidad de color

- Tipo de carta *(Tierra, Conjuro, Instantáneo, Encantamiento, Criatura, Planeswalker)*

- Posibles supertipos *(Legendary, Basic)*

- Indicador de si la carta está prohibida en el formato Commander

Las cartas no son todas iguales. Algunos tipos pueden poseer propiedades específicas. Por ejemplo:

- Todas las <u>**NO**</u> ***Tierras*** contienen valor de Maná *(Específico y General)*

- Las ***Criaturas*** poseen fuerza y resistencia.

- Los ***Planeswalkers*** poseen lealtad.

El sistema deberá reflejar estas diferencias entre tipos de cartas en su diseño.

---

### 📦 3. El Almacén del Archivista

> *“Un archivo desordenado es solo caos disfrazado de conocimiento.”*

El sistema mantendrá un almacén central de cartas.

Reglas del almacén:

- Solo puede existir una copia de cada carta dentro del archivo.

- Las cartas deben poder identificarse de forma única dentro del sistema.

El sistema deberá permitir las siguientes operaciones:

- Registrar cartas

- Modificar cartas

- Eliminar cartas

- Buscar una carta concreta por ***Nombre***

Además, deberá poder mostrar listados de cartas filtrados por distintos criterios.

Los filtros disponibles deberán permitir seleccionar:

- Un supertipo concreto

- Un tipo de carta

- Una identidad de color

---

### 🧠 4. La Cámara de Estrategias (Decks de Commander)

> *“La victoria rara vez depende de una sola carta, sino de cómo todas trabajan juntas.”*

El sistema también deberá permitir gestionar mazos del formato Commander.

Cada mazo deberá contener:

- Un título

- Un comandante

- Una lista de cartas

Reglas del formato Commander:

- Un mazo debe contener **exactamente 100 cartas**.

- Debe existir un comandante:

    - El comandante debe ser una ***Criatura Legendaria*** válida.

    - El comandante debe formar parte del propio mazo.

Restricciones adicionales:

- Las cartas utilizadas en un mazo deben existir previamente en el archivo de cartas.

- <u>**NO**</u> se permiten cartas repetidas dentro de un mazo, excepto en el caso de ***Tierras Básicas***.

- Las cartas ***Prohibidas*** en Commander <u>**NO**</u> pueden incluirse en un mazo.

- Todas las cartas del mazo deben respetar la identidad de color del comandante.

El sistema deberá permitir:

- Crear mazos

- Modificar mazos (Cambiar de Nombre o Cambiar de Comandante)

- Añadir o retirar cartas

- Eliminar mazos

La consulta de mazos se realizará mediante búsquedas que permitan:

- Buscar un mazo por título

- Buscar un mazo por comandante

---

### 📊 5. Informes del Archivo

> *“Solo quien comprende su colección conoce realmente su potencial.”*

El sistema deberá poder generar informes generales sobre el estado del archivo.

Entre otros datos, deberá ser posible conocer:

- Número total de cartas registradas

- Número de cartas prohibidas

- Número de cartas por tipo

- Identidad de color con mayor número de cartas

- Número total de mazos registrados

Estos informes permitirán evaluar el estado general del archivo y su contenido.

---

### 💾 6. El Grimorio Persistente (Persistencia de Datos)

> *“El conocimiento que no se preserva termina desapareciendo.”*

La información del sistema deberá mantenerse entre ejecuciones del programa.

Funcionamiento esperado:

- Al iniciar el programa:

    - Se cargarán los datos desde archivos externos.

- Durante la ejecución:

    - El programa trabajará con los datos en memoria.

- Al cerrar el programa:

    - Se comprobará si los datos actuales difieren de los originalmente cargados.

    - Si existen cambios sin guardar, el sistema deberá:

        - Avisar al usuario.

        - Permitir elegir entre:

            - Salir sin guardar

            - Guardar los cambios antes de cerrar

---

⚠️ 7. Reglas del Archivo

> *“El orden no limita el conocimiento; lo hace posible.”*

El sistema deberá cumplir las siguientes condiciones:

- Integridad:

    - Los mazos deben respetar siempre las reglas del formato Commander.

    - Las cartas prohibidas no pueden formar parte de un mazo.

- Validación:

    - Los datos introducidos por el usuario deben verificarse antes de almacenarse.

- Organización:

    - El código deberá estructurarse de forma que facilite la gestión de cartas, mazos y consultas.

---

Made-By: Álvaro Ruiz

GitHub-User: Phoenix2332