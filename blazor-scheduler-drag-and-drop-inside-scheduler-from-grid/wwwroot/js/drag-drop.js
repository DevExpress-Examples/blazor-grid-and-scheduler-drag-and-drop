let dotNetHelper = null;
let isDragging = false;
let currentHandle = null;
let dropZones = [];
let currentHighlightedCell = null;
const highlightClass = 'dnd-highlight-cell';

export function initialize(dotNetRef) {
    dotNetHelper = dotNetRef;
}

export function startDragging(handleSelector) {
    const handles = document.querySelectorAll(handleSelector);
    
    handles.forEach(handle => {
        handle.addEventListener('mousedown', handleMouseDown);
    });
}

export function registerDropZone(dropZoneSelector, cellSelector) {
    dropZones.push({
        selector: dropZoneSelector,
        cellSelector: cellSelector
    });
}

export function updateDragPreviewPosition(x, y, width, height, backgroundColor) {
    const preview = document.querySelector('.drag-preview');
    if (preview) {
        preview.style.left = `${x}px`;
        preview.style.top = `${y}px`;
        preview.style.width = `${width}px`;
        preview.style.height = `${height}px`;
        preview.style.backgroundColor = backgroundColor;
    }
}

function handleMouseDown(e) {
    if (e.button !== 0) return;
    
    e.preventDefault();
    isDragging = true;
    currentHandle = e.currentTarget;
    
    const x = e.clientX;
    const y = e.clientY;
    
    const itemIndex = currentHandle.getAttribute('data-item-index');
    const index = itemIndex ? parseInt(itemIndex, 10) : null;
    
    if (dotNetHelper) {
        dotNetHelper.invokeMethodAsync('NotifyDragStart', x, y, index);
    }
    
    document.addEventListener('mousemove', handleMouseMove);
    document.addEventListener('mouseup', handleMouseUp);
}

function handleMouseMove(e) {
    if (!isDragging) return;
    
    e.preventDefault();
    
    const x = e.clientX;
    const y = e.clientY;
    
    const itemIndex = currentHandle?.getAttribute('data-item-index');
    const index = itemIndex ? parseInt(itemIndex, 10) : null;
    
    if (dotNetHelper) {
        dotNetHelper.invokeMethodAsync('NotifyDragMove', x, y, index);
    }
    
    checkDropZoneCell(x, y);
}

function checkDropZoneCell(x, y) {
    const dragPreview = document.querySelector('.drag-preview');
    let wasVisible = false;
    if (dragPreview) {
        wasVisible = dragPreview.style.display !== 'none';
        dragPreview.style.pointerEvents = 'none'; 
    }
    
    const element = document.elementFromPoint(x, y);
    
    if (!element) {
        clearHighlight();
        return;
    }
    
    let foundCell = false;
    
    for (const zone of dropZones) {
        const dropZoneElement = document.querySelector(zone.selector);
        if (!dropZoneElement) {
            console.warn(`Drop zone not found: ${zone.selector}`);
            continue;
        }
        
        if (dropZoneElement.contains(element)) {
            const cell = element.closest(zone.cellSelector);
            
            if (cell) {
                foundCell = true;
                
                if (cell !== currentHighlightedCell) {
                    clearHighlight();
                    currentHighlightedCell = cell;
                    cell.classList.add(highlightClass);
                    
                    const cellInfo = getCellInfo(cell);
                    
                    if (dotNetHelper) {
                        dotNetHelper.invokeMethodAsync('NotifyDragEnterDropZone', cellInfo);
                    }
                }
                break; 
            }
        }
    }
    
    if (!foundCell && currentHighlightedCell) {
        clearHighlight();
        if (dotNetHelper) {
            dotNetHelper.invokeMethodAsync('NotifyDragLeaveDropZone');
        }
    }
}

function getCellInfo(cell) {
    const info = {
        className: cell.className,
        dataAttributes: {}
    };
    
    for (const attr of cell.attributes) {
        if (attr.name.startsWith('data-')) {
            let value = attr.value;
            
            if ((attr.name === 'data-start' || attr.name === 'data-end') && value) {
                try {
                    const dateValue = new Date(value);
                    if (!isNaN(dateValue.getTime())) {
                        value = dateValue.toISOString();
                    }
                } catch (e) {
                    console.warn(`Failed to parse date from ${attr.name}: ${value}`, e);
                }
            }
            
            info.dataAttributes[attr.name] = value;
        }
    }
    
    console.log('Cell info:', info); 
    return JSON.stringify(info);
}

function clearHighlight() {
    if (currentHighlightedCell) {
        currentHighlightedCell.classList.remove(highlightClass);
        currentHighlightedCell = null;
    }
}

function handleMouseUp(e) {
    if (!isDragging) return;
    
    isDragging = false;
    
    const x = e.clientX;
    const y = e.clientY;
    
    const itemIndex = currentHandle?.getAttribute('data-item-index');
    const index = itemIndex ? parseInt(itemIndex, 10) : null;
    
    if (dotNetHelper) {
        dotNetHelper.invokeMethodAsync('NotifyDragEnd', x, y, index);
    }
    
    clearHighlight();
    
    document.removeEventListener('mousemove', handleMouseMove);
    document.removeEventListener('mouseup', handleMouseUp);
    currentHandle = null;
}

export function dispose() {
    document.removeEventListener('mousemove', handleMouseMove);
    document.removeEventListener('mouseup', handleMouseUp);
    
    const handles = document.querySelectorAll('.drag-handle');
    handles.forEach(handle => {
        handle.removeEventListener('mousedown', handleMouseDown);
    });
    
    clearHighlight();
    dropZones = [];
    dotNetHelper = null;
    isDragging = false;
    currentHandle = null;
}
