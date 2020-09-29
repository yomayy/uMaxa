var app = new Vue({
	el: '#app',
	data: {
		editing: false,
		loading: false,
		addProductFlag: false,
		objectIndex: 0,
		categoryModel: {
			id: 00000000 - 0000 - 0000 - 0000 - 000000000000,
			name: "CategoryName",
			description: "Category Description"
		},
		productModel: {
			id: 00000000 - 0000 - 0000 - 0000 - 000000000000,
			name: "ProductName",
			description: "Product Description",
			value: 1.99,
			image: "",
			categoryId: 0
		},
		categories: [],
		//selectedCategory: {
		//	id: 0,
		//	name: "",
		//	description: "",
		//	products: []
		//},
		selectedCategory: null,
		newProduct: {
			categoryId: 0,
			name: "Product name",
			description: "Product description",
			value: 1.99,
			image: ""
		},
		selectedFile: null,
	},
	mounted() {
		this.getCategories();
	},
	methods: {
		onFileSelected(event) {
			console.log(event);
			this.selectedFile = event.target.files[0];
			//
			const file = event.target.files[0];
			const reader = new FileReader();
			reader.onloadend = () => {
				if (reader.result.slice(0, 10) === 'data:image') {
					console.log('Result', reader.result);
					this.productModel.image = reader.result;
				}
			};
			if (file) {
				reader.readAsDataURL(file);
			}
		},
		getCategory(id) {
			this.loading = true
			axios.get('/categories/' + id)
				.then(res => {
					console.log(res);
					var category = res.data;
					this.categoryModel = {
						id: category.id,
						name: category.name,
						description: category.description
					};
				})
				.catch(err => {
					console.log(err);
				})
				.then(() => {
					this.loading = false;
				});
		},
		getCategories() {
			this.loading = true
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
		createCategory() {
			this.loading = true;
			axios.post('/categories', this.categoryModel)
				.then(res => {
					console.log(res.data);
					this.categories.push(res.data);
				})
				.catch(err => {
					console.log(err);
				})
				.then(() => {
					this.loading = false;
					this.editing = false;
					this.getCategories();
				})
		},
		updateCategory() {
			this.loading = true;
			axios.put('/categories', this.categoryModel)
				.then(res => {
					console.log(res.data);
					this.categories.splice(this.objectIndex, 1, res.data);
				})
				.catch(err => {
					console.log(err);
				})
				.then(() => {
					this.getCategories();
					this.loading = false;
					this.editing = false;
				})
		},
		deleteCategory(id, index) {
			this.loading = true
			axios.delete('/categories/' + id)
				.then(res => {
					console.log(res);
					this.categories.splice(index, 1);
				})
				.catch(err => {
					console.log(err);
				})
				.then(() => {
					this.loading = false;
				});
		},
		newCategory() {
			this.editing = true;
			this.categoryModel.id = 0;
		},
		editCategory(id, index) {
			this.objectIndex = index;
			this.getCategory(id);
			this.editing = true;
		},
		cancel() {
			this.editing = false;
		},
		selectCategory(category) {
			console.log(category);
			this.selectedCategory = category;
			this.newProduct.categoryId = category.id;
		},
		///
		/// Product
		///
		newCatProduct() {
			this.editing = true;
			this.addProductFlag = true;
			this.productModel.id = 0;
			this.productModel.categoryId = this.selectedCategory.id;
		},
		createProduct() {
			this.loading = true;
			axios.post('/categories/' + this.selectedCategory.id + '/products', this.productModel)
				.then(res => {
					console.log(res.data);
					this.selectedCategory.products.push(res.data);
				})
				.catch(err => {
					console.log(err);
				})
				.then(() => {
					this.loading = false;
					this.editing = false;
					this.getCategories();
				})
		},
		deleteProduct(id, index) {
			this.loading = true
			axios.delete('/products/' + id)
				.then(res => {
					console.log(res);
					this.selectedCategory.products.splice(index, 1);
				})
				.catch(err => {
					console.log(err);
				})
				.then(() => {
					this.loading = false;
				});
		},
		updateProduct() {
			this.loading = true;
			axios.put('/categories/' + this.selectedCategory.id + '/products', {
				products: this.selectedCategory.products.map(x => {
					return {
						id: x.id,
						name: x.name,
						description: x.description,
						value: x.value,
						createdOn: x.createdOn,
						modifiedOn: x.modifiedOn,
						categoryId: this.selectedCategory.id
					};
				})
			})
				.then(res => {
					console.log(res);
					this.selectedCategory.products.splice(index, 1);
				})
				.catch(err => {
					console.log(err);
				})
				.then(() => {
					this.loading = false;
				});
		}
	},
	computed: {
	}
})