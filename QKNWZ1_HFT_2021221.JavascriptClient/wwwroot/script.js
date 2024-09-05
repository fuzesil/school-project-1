let expertgroups = [];
let countries = [];
let members = [];
let connection = null;
getExpertgroups();
getCountries();
getMembers();
setupSignalR();

// ---- START-UP ---- //
function setupSignalR() {
	connection = new signalR.HubConnectionBuilder()
		.withUrl("http://localhost:49943/hub")
		.configureLogging(signalR.LogLevel.Debug)
		.build();

	connection.on("PutExpertGroup", (user, message) => {
		console.log("Received PutExpertgroup with " + user + " and " + message);
		getExpertgroups();
	});

	connection.on("DeleteExpertGroup", (user, message) => {
		console.log("Received DeleteExpertgroup with " + user + " and " + message);
		getExpertgroups();
	});

	connection.on("PostExpertGroup", (user, message) => {
		console.log("Received PostExpertgroup with " + user + " and " + message);
		getExpertgroups();
	});

	connection.on("PutCountry", (user, message) => {
		console.log("Received PutCountry with " + user + " and " + message);
		getCountries();
	});

	connection.on("DeleteCountry", (user, message) => {
		console.log("Received DeleteCountry with " + user + " and " + message);
		getCountries();
	});

	connection.on("PostCountry", (user, message) => {
		console.log("Received PostCountry with " + user + " and " + message);
		getCountries();
	});

	connection.on("PutMember", (user, message) => {
		console.log("Received PutMember with " + user + " and " + message);
		getMembers();
	});

	connection.on("DeleteMember", (user, message) => {
		console.log("Received DeleteMember with " + user + " and " + message);
		getMembers();
	});

	connection.on("PostMember", (user, message) => {
		console.log("Received PostMember with " + user + " and " + message);
		getMembers();
	});

	connection.onclose(async () => {
		await start();
	});
	start();
}

async function start() {
	try {
		await connection.start();
		console.log("SignalR Connected.");
	} catch (err) {
		console.log(err);
		setTimeout(start, 5000);
	}
};

// ---- Get All ---- //
async function getExpertgroups() {
	await fetch('http://localhost:49943/Internal/GetExpertgroupTable')
		.then(response => response.json())
		.then(y => {
			expertgroups = y;
			console.log(expertgroups);
		});
	expertgroupDisplay();
}

async function getSingleExpertgroup() {
	expertgroups = [];
	let name = document.getElementById('expertgroupname').value;
	console.log('GET Expert group: ' + name);
	await fetch('http://localhost:49943/Internal/GetOneExpertGroup/' + name)
		.then(response => response.json())
		.then(y => {
			expertgroups.push(y);
			console.log(y);
		});
	expertgroupDisplay();
}

async function getCountries() {
	await fetch('http://localhost:49943/Internal/GetCountryTable')
		.then(response => response.json())
		.then(y => {
			countries = y;
			console.log(countries);
		});
	countryDisplay();
}

async function getSingleCountry() {
	countries = [];
	let name = document.getElementById('countryname').value;
	console.log('GET Country: ' + name);
	await fetch('http://localhost:49943/Internal/GetOneCountry/' + name)
		.then(response => response.json())
		.then(y => {
			countries.push(y);
			console.log(y);
		});
	countryDisplay();
}

async function getMembers() {
	await fetch('http://localhost:49943/Internal/GetMemberTable')
		.then(response => response.json())
		.then(y => {
			members = y;
			console.log(members);
		});
	memberDisplay();
}

async function getSingleMember() {
	members = [];
	let name = document.getElementById('membername').value;
	console.log('GET Member: ' + name);
	await fetch('http://localhost:49943/Internal/GetOneMember/' + name)
		.then(response => response.json())
		.then(y => {
			members.push(y);
			console.log(y);
		});
	memberDisplay();
}

// ---- Display functions ---- //
function expertgroupDisplay() {
	document.getElementById('expertgrouparea').innerHTML = "";
	expertgroups.forEach(eg => {
		document.getElementById('expertgrouparea').innerHTML += "<tr>"
			+ "<td>" + eg.id + "</td><td>" + eg.name + "</td><td>"
			+ `<button type="button" onclick="deleteExpertgroup(${eg.id})">Delete ${eg.name}</button>`
			+ `<button type="button" onclick="putExpertgroup(${eg.id})">Update ${eg.name}</button>` + "</td>"
			+ "</tr>";
	});
}

function countryDisplay() {
	document.getElementById('countryarea').innerHTML = "";
	countries.forEach(country => {
		document.getElementById('countryarea').innerHTML += "<tr>"
			+ "<td>" + country.id + "</td><td>" + country.name + "</td><td>" + country.capitalCity + "</td><td>" + country.callingCode + "</td><td>"
			+ `<button type="button" onclick="deleteCountry(${country.id})">Delete ${country.name}</button>`
			+ `<button type="button" onclick="putCountry(${country.id})">Update ${country.name}</button>` + "</td>"
			+ "</tr>";
	});
}

function memberDisplay() {
	document.getElementById('memberarea').innerHTML = "";
	members.forEach(member => {
		document.getElementById('memberarea').innerHTML += "<tr>"
			+ "<td>" + member.id + "</td><td>" + member.name + "</td><td>" + member.website + "</td><td>"
			+ member.publisher + "</td><td>" + member.phoneNumber + "</td><td>" + member.officeLocation + "</td><td>"
			+ `<button type="button" onclick="deleteMember(${member.id})">Delete ${member.name}</button>`
			+ `<button type="button" onclick="putMember(${member.id})">Update ${member.name}</button>` + "</td>"
			+ "</tr>";
	});
}

// ---- Expert group ---- //
function deleteExpertgroup(id) {
	console.log('Attempting DELETE for Expert group ID: ', id);
	fetch('http://localhost:49943/Admin/DeleteExpertGroup/' + id, {
		method: 'DELETE',
		headers: { 'Content-Type': 'application/json', },
		body: null
	})
		.then(response => response)
		.then(data => {
			console.log('Success:', data);
		})
		.catch((error) => { console.error('Error:', error); });
}

function postExpertgroup() {
	let name = document.getElementById('expertgroupname').value;
	console.log('Post Expert group input: ' + name);
	fetch('http://localhost:49943/Admin/PostExpertGroup', {
		method: 'POST',
		headers: { 'Content-Type': 'application/json', },
		body: JSON.stringify(
			{ Name: name })
	})
		.then(response => response)
		.then(data => {
			console.log('Success:', data);
		})
		.catch((error) => { console.error('Error:', error); });

}

function putExpertgroup(id) {
	let name = document.getElementById('expertgroupname').value;
	console.log('Put Expert group input: ' + name);
	fetch('http://localhost:49943/Admin/PutExpertGroup', {
		method: 'PUT',
		headers: { 'Content-Type': 'application/json', },
		body: JSON.stringify(
			{ Id: id, Name: name })
	})
		.then(response => response)
		.then(data => {
			console.log('Success:', data);
		})
		.catch((error) => { console.error('Error:', error); });
}

// ---- Country ---- //
function postCountry() {
	let name = document.getElementById('countryname').value;
	console.log('Post Country input: ' + name);
	console.log('User input: ' + name);
	fetch('http://localhost:49943/Admin/PostCountry', {
		method: 'POST',
		headers: { 'Content-Type': 'application/json', },
		body: JSON.stringify(
			{ Name: name })
	})
		.then(response => response)
		.then(data => {
			console.log('Success:', data);
		})
		.catch((error) => { console.error('Error:', error); });
}

function deleteCountry(id) {
	console.log('Delete Country input: ' + id);
	fetch('http://localhost:49943/Admin/DeleteCountry/' + id, {
		method: 'DELETE',
		headers: { 'Content-Type': 'application/json', },
		body: null
	})
		.then(response => response)
		.then(data => {
			console.log('Success:', data);
		})
		.catch((error) => { console.error('Error:', error); });
}

function putCountry(id) {
	let name = document.getElementById('countryname').value;
	console.log('Put Country input: ' + name);
	fetch('http://localhost:49943/Admin/PutCountry', {
		method: 'PUT',
		headers: { 'Content-Type': 'application/json', },
		body: JSON.stringify(
			{ Id: id, Name: name })
	})
		.then(response => response)
		.then(data => {
			console.log('Success:', data);
		})
		.catch((error) => { console.error('Error:', error); });
}

// ---- Member ---- //
function postMember() {
	let name = document.getElementById('membername').value;
	console.log('Post Member input: ' + name);
	console.log('User input: ' + name);
	fetch('http://localhost:49943/Admin/PostMember', {
		method: 'POST',
		headers: { 'Content-Type': 'application/json', },
		body: JSON.stringify(
			{ Name: name })
	})
		.then(response => response)
		.then(data => {
			console.log('Success:', data);
		})
		.catch((error) => { console.error('Error:', error); });
}

function deleteMember(id) {
	console.log('Delete Member input: ' + id);
	fetch('http://localhost:49943/Admin/DeleteMember/' + id, {
		method: 'DELETE',
		headers: { 'Content-Type': 'application/json', },
		body: null
	})
		.then(response => response)
		.then(data => {
			console.log('Success:', data);
		})
		.catch((error) => { console.error('Error:', error); });
}

function putMember(id) {
	let name = document.getElementById('membername').value;
	console.log('Put Member input: ' + name);
	fetch('http://localhost:49943/Admin/PutMember', {
		method: 'PUT',
		headers: { 'Content-Type': 'application/json', },
		body: JSON.stringify(
			{ Id: id, Name: name })
	})
		.then(response => response)
		.then(data => {
			console.log('Success:', data);
		})
		.catch((error) => { console.error('Error:', error); });
}
