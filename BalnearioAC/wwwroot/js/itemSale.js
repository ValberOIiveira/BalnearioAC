import { apiRequest } from './script.js';

async function GetItemSale(event){

    ItemSale = await apiRequest('http://localhost:5237/ItemsSale', 'GET')

    console.log(ItemSale);
}


async function PostItemSale(event){

    const SaleId = document.getElementById('SaleId').value;
    const ProductId = document.getElementById('ProductId').value;
    const Quantity = document.getElementById('Quantity').value;

}



