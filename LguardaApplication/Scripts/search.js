function highlightSearchText(searchText, table) {
    var container = document.getElementById(table);
    dehighlight(container);
    if (searchText.value.length > 0)
        highlight(searchText.value.toLowerCase(), container);
}

/*
* Transform back each
* <span>preText <span class="highlighted">searchText</span> postText</span>
* into its original
* preText Stxt postText
*/
function dehighlight(container) {
    for (var i = 0; i < container.childNodes.length; i++) {
        var node = container.childNodes[i];
        if (node.attributes && node.attributes['class'] && node.attributes['class'].value == 'highlighted') {
            node.parentNode.parentNode.replaceChild(document.createTextNode(node.parentNode.innerHTML.replace(/<[^>]+>/g, "")), node.parentNode);
            // Stop here and process next parent
            return;
        } else if (node.nodeType != 3) {
            // Keep going onto other elements
            dehighlight(node);
        }
    }
}
/*
* Create a
* <span>preText <span class="highlighted">searchText</span> postText</span>
* around each search searchText
*/
function highlight(searchText, container) {
    for (var i = 0; i < container.childNodes.length; i++) {
        var node = container.childNodes[i];
        if (node.nodeType == 3) {
            // Text node
            var data = node.data.trim();
            var data_low = data.toLowerCase();
            if (data_low.indexOf(searchText) >= 0) {
                //Stxt found!
                var new_node = document.createElement('span');
                node.parentNode.replaceChild(new_node, node);
                var result;
                while ((result = data_low.indexOf(searchText)) != -1) {
                    new_node.appendChild(document.createTextNode(data.substr(0, result)));
                    new_node.appendChild(create_node(document.createTextNode(data.substr(result, searchText.length))));
                    data = data.substr(result + searchText.length);
                    data_low = data_low.substr(result + searchText.length);
                }
                new_node.appendChild(document.createTextNode(data));
            }
        } else {
            // Keep going onto other elements
            highlight(searchText, node);
        }
    }
}
function create_node(child) {
    var node = document.createElement('span');
    node.setAttribute('class', 'highlighted');
    node.attributes['class'].value = 'highlighted';
    node.appendChild(child);
    return node;
}
