var app = new Vue({
	el: '#app',
	data: {
		editing: false,
		loading: false,
		objectIndex: 0,
		productModel: {
			id: 00000000 - 0000 - 0000 - 0000 - 000000000000,
			name: "ProductName",
			description: "Product Description",
			value: 1.99,
			category: null
		},
		products: [],
		categories: [],
		selectedFile: null,
	},
	mounted() {
		this.getProducts();
	},
	methods: {
		onFileSelected(event) {
			console.log(event);
			this.selectedFile = event.target.files[0];
		},
		onUpload() {

		},
		getProduct(id) {
			this.loading = true
			axios.get('/products/' + id)
				.then(res => {
					console.log(res);
					var product = res.data;
					this.productModel = {
						id: product.id,
						name: product.name,
						description: product.description,
						value: product.value,
						category: product.category
					};
				})
				.catch(err => {
					console.log(err);
				})
				.then(() => {
					this.loading = false;
				});
		},
		getProducts() {
			this.loading = true
			axios.get('/products')
				.then(res => {
					console.log(res);
					this.products = res.data;
				})
				.catch(err => {
					console.log(err);
				})
				.then(() => {
					this.getCategories();
					this.loading = false;
				});
		},
		getCategories() {
			this.loading = true;
			axios.get('/categories')
				.then(res => {
					console.log(res);
					this.categories = res.data;
				})
				.catch(err => {
					console.log(err);
				})
				.then(() => {
					this.loading = false;
				});
		},
		createProduct() {
			this.loading = true;
			axios.post('/products', this.productModel)
				.then(res => {
					console.log(res.data);
					this.products.push(res.data);
				})
				.catch(err => {
					console.log(err);
				})
				.then(() => {
					this.loading = false;
					this.editing = false;
				})
		},
		updateProduct() {
			this.loading = true;
			axios.put('/products', this.productModel)
				.then(res => {
					console.log(res.data);
					this.products.splice(this.objectIndex, 1, res.data);
				})
				.catch(err => {
					console.log(err);
				})
				.then(() => {
					this.getProducts();
					this.loading = false;
					this.editing = false;
				})
		},
		deleteProduct(id, index) {
			this.loading = true
			axios.delete('/products/' + id)
				.then(res => {
					console.log(res);
					this.products.splice(index, 1);
				})
				.catch(err => {
					console.log(err);
				})
				.then(() => {
					this.loading = false;
				});
		},
		newProduct() {
			this.editing = true;
			this.productModel.id = 0;
			this.productModel.category = this.categories[0];
		},
		editProduct(id, index) {
			this.objectIndex = index;
			this.getProduct(id);
			this.categoryName = this.productModel.category.name;
			this.editing = true;
		},
		cancel() {
			this.editing = false;
		}
	},
	computed: {
	}
})