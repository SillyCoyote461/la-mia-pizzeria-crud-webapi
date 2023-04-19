//axios.get('./Api/Pizza/Get')
//    .then(function (response) {

//        console.log(response);
//    })

const initialize = filter => getPizzas(filter).then(renderCards);

const getPizzas = filter => axios.get('/Api/Pizza/Get', filter ? { params: { filter } } : {}).then(res => res.data );

const renderCards = pizzas => {
    const table = document.getElementById('table');
    table.innerHTML = pizzas.map(cardBody).join('');
    
}


const cardBody = pizza => `                
                 <div class="card mb-5 bg-black bg-opacity-10" style="width: 18rem;">
                    <img src="${pizza.image}" class="card-img-top" alt="...">
                    <div class="card-body">
                        <h5 class="card-title">${pizza.name}</h5>
                        <p class="card-text">${pizza.description}</p>
                    </div>
                    <div class="card-body">

                            <p class="m-0"><b>Categoria: </b> </p>


                        <p class="m-0"><b>Prezzo: </b> ${pizza.price} €</p>
                    </div>
                    <div class="card-body">
                        <a href="/Pizza/Details/${pizza.id}" class="btn btn-primary">Details</a>

                            <a href="/Pizza/Update/${pizza.id}" class="btn btn-success">Update</a>
                            <button onClick="deletePizza(${pizza.id})" type="submit" class="btn btn-danger">
                                Delete
                            </button>

                    </div>
                </div>
`;


//create form section
//init create
const initializeCreate = () => {
    document.getElementById("create-form").addEventListener("submit", function (event) {
        event.preventDefault()
    });

    document.getElementById("create-form").addEventListener("submit", function () {
        var pizza = dataForm();
        saveData(pizza)
    })
}

//get date create
function dataForm(){
    var name = document.getElementById("name-form").value
    var description = document.getElementById("description-form").value
    //var ingredients = document.getElementById("ingredients-form").value
    var price = document.getElementById("price-form").value
    //var categoryId = document.getElementById("category-form").value
    var image = document.getElementById("image-form").value

    return {
        id: 0,
        name,
        description,
        //ingredients,
        price,
        //categoryId,
        image
    }
}

//save data create
const saveData = pizza => {
    axios.post('/Api/Pizza/CreatePizza', pizza)
        .then(() => location.href = "/Pizza/ApiIndex")
        .catch(function (error) {
            console.log(error)
        })
}


//delete pizza
function deletePizza(id) {
    axios.delete(`/Api/Pizza/Delete/${id}`)
        .then(function (response) {
            console.log(response)
        }).catch(function (error) {
            console.log(error)
        });
    location.reload()
}