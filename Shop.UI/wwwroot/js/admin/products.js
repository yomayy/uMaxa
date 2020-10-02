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
			category: null,
			image: ""
		},
		products: [],
		categories: [],
		selectedFile: null,
		image: "",
		pageNumber: 1,
		pageSize: 3,
		productsCount: 1,
		totalPages: 0
	},
	mounted() {
		this.getProductsCount();
		this.getProducts();
	},
	computed: {
		// todo: create paging here
	},
	methods: {
		onFileSelected(event) {
			console.log(event);
			this.selectedFile = event.target.files[0];
			//
			const file = event.target.files[0];
			const reader = new FileReader();
			const fileLimit = 1000000;

			if (file.size <= fileLimit) {
				reader.onloadend = () => {
					if (reader.result.slice(0, 10) === 'data:image') {
						console.log('Result', reader.result);
						this.image = reader.result;
						this.productModel.image = this.image;
					}
					else {
						alert('not image');
					}
				};
				if (file) {
					reader.readAsDataURL(file);
				}
			} else {
				alert('to big file selected');
			}
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
						image: product.image,
						category: product.category
					};
					this.categoryName = this.productModel.category.name;
				})
				.catch(err => {
					console.log('getProduct err', err);
				})
				.then(() => {
					this.loading = false;
				});
		},
		setTotalPages() {
			this.totalPages = Math.ceil(this.productsCount / this.pageSize);
		},
		getProductsCount() {
			this.loading = true
			axios.get('/products/count')
				.then(res => {
					console.log(res);
					this.productsCount = res.data;
					//this.totalPages = Math.ceil(this.productsCount / this.pageSize);
					this.setTotalPages();
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
			axios.get('/products',
				{
					params: {
						pageNumber: this.pageNumber,
						pageSize: this.pageSize
					}
				})
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
					this.productsCount++;
					this.setTotalPages();
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
					this.productsCount--;
					this.setTotalPages();
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
			this.productModel.image = "";
			this.productModel.category = this.categories[0];
		},
		editProduct(id, index) {
			this.objectIndex = index;
			this.getProduct(id);
			this.editing = true;
		},
		cancel() {
			this.editing = false;
		},
		nextPage() {
			this.pageNumber++;
			this.getProducts();
		},
		prevPage() {
			this.pageNumber--;
			this.getProducts();
		}
	}
})