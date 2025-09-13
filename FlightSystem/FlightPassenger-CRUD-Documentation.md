# FlightPassenger CRUD Operations Documentation

## Overview
Complete CRUD (Create, Read, Update, Delete) operations have been implemented for FlightPassenger management, allowing full control over flight bookings and passenger assignments.

## 🏗️ Architecture Components

### 1. Data Transfer Objects (DTOs)

#### Request DTOs
- **`CreateFlightPassengerDto`** - Create new booking with FlightId and PassengerId
- **`CreateFlightPassengerByPassportDto`** - Create new booking using passport number
- **`UpdateFlightPassengerDto`** - Update booking details (special requests, baggage info)

#### Response DTOs
- **`FlightPassengerDto`** - Complete booking information with passenger and flight details
- **`FlightPassengerValidationDto`** - Validation response with booking status

### 2. Service Layer
- **`IFlightPassengerService`** - Interface defining all CRUD operations
- **`FlightPassengerService`** - Implementation with business logic and validation

### 3. Controller Layer
- **`FlightPassengerController`** - REST API endpoints for all CRUD operations

### 4. UI Layer (CheckinApp)
- **`FlightPassengerManagementForm`** - Main management interface
- **`CreateFlightPassengerForm`** - Create new bookings
- **`UpdateFlightPassengerForm`** - Update existing bookings
- **`OptionsForm`** - Entry point with options menu

## 📋 CRUD Operations

### ✅ CREATE Operations

#### 1. Create FlightPassenger by ID
```http
POST /api/flightpassenger
Content-Type: application/json

{
  "flightId": 123,
  "passengerId": 456,
  "bookingReference": "ABC123456",
  "specialRequests": "Vegetarian meal",
  "baggageInfo": "1 checked bag, 1 carry-on",
  "createdByEmployeeId": 789
}
```

#### 2. Create FlightPassenger by Passport
```http
POST /api/flightpassenger/by-passport
Content-Type: application/json

{
  "flightId": 123,
  "passportNumber": "AB1234567",
  "bookingReference": "XYZ789012",
  "specialRequests": "Wheelchair assistance",
  "baggageInfo": "2 checked bags",
  "createdByEmployeeId": 789
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "id": 1001,
    "bookingReference": "ABC123456",
    "isCheckedIn": false,
    "checkinTime": null,
    "specialRequests": "Vegetarian meal",
    "baggageInfo": "1 checked bag, 1 carry-on",
    "passenger": {
      "id": 456,
      "passportNumber": "AB1234567",
      "firstName": "John",
      "lastName": "Doe",
      "nationality": "US",
      "dateOfBirth": "1990-01-01",
      "passengerType": "Adult",
      "email": "john.doe@email.com",
      "phone": "+1234567890"
    },
    "flight": {
      "id": 123,
      "flightNumber": "FL001",
      "departureAirport": "LAX",
      "arrivalAirport": "JFK",
      "scheduledDeparture": "2024-01-15T10:00:00Z",
      "scheduledArrival": "2024-01-15T18:00:00Z",
      "status": "Scheduled",
      "gateNumber": "A12"
    }
  },
  "message": "Нислэгийн бүртгэл амжилттай үүсгэгдлээ"
}
```

### 📖 READ Operations

#### 1. Get All FlightPassengers
```http
GET /api/flightpassenger
```

#### 2. Get FlightPassenger by ID
```http
GET /api/flightpassenger/1001
```

#### 3. Get FlightPassengers by Flight
```http
GET /api/flightpassenger/flight/123
```

#### 4. Get FlightPassengers by Passenger
```http
GET /api/flightpassenger/passenger/456
```

#### 5. Get FlightPassenger by Booking Reference
```http
GET /api/flightpassenger/booking/ABC123456
```

#### 6. Validate FlightPassenger
```http
GET /api/flightpassenger/validate/123/AB1234567
```

### ✏️ UPDATE Operations

#### Update FlightPassenger
```http
PUT /api/flightpassenger/1001
Content-Type: application/json

{
  "specialRequests": "Updated: Vegetarian meal, window seat preferred",
  "baggageInfo": "Updated: 2 checked bags, 1 carry-on",
  "updatedByEmployeeId": 789
}
```

**Response:**
```json
{
  "success": true,
  "data": {
    "id": 1001,
    "bookingReference": "ABC123456",
    "isCheckedIn": false,
    "checkinTime": null,
    "specialRequests": "Updated: Vegetarian meal, window seat preferred",
    "baggageInfo": "Updated: 2 checked bags, 1 carry-on",
    // ... passenger and flight details
  },
  "message": "Нислэгийн бүртгэл амжилттай шинэчлэгдлээ"
}
```

### 🗑️ DELETE Operations

#### 1. Delete FlightPassenger (Hard Delete)
```http
DELETE /api/flightpassenger/1001
```

**Response:**
```json
{
  "success": true,
  "data": true,
  "message": "Нислэгийн бүртгэл амжилттай устгагдлаа"
}
```

#### 2. Cancel FlightPassenger (Soft Cancel)
```http
POST /api/flightpassenger/1001/cancel
Content-Type: application/json

789
```

**Response:**
```json
{
  "success": true,
  "data": true,
  "message": "Нислэгийн бүртгэл амжилттай цуцлагдлаа"
}
```

## 🔒 Business Logic & Validation

### Create Validation
- ✅ Flight must exist and be valid
- ✅ Passenger must exist
- ✅ Passenger cannot be booked twice for same flight
- ✅ Booking reference must be unique
- ✅ Employee must exist (for audit trail)

### Update Validation
- ✅ FlightPassenger must exist
- ✅ Only editable fields can be updated (special requests, baggage info)
- ✅ Core booking data (flight, passenger, booking reference) cannot be changed
- ✅ Employee must exist (for audit trail)

### Delete Validation
- ✅ FlightPassenger must exist
- ✅ Cannot delete if passenger is checked in (must cancel first)
- ✅ Cascading delete of related records (seat assignments, boarding passes)

### Cancel Validation
- ✅ FlightPassenger must exist
- ✅ If checked in, releases seat assignments and boarding passes
- ✅ Resets check-in status without deleting the booking record

## 🖥️ User Interface Features

### FlightPassengerManagementForm
- **📋 List View**: All bookings with sorting and filtering
- **🔍 Details Panel**: Complete booking information
- **➕ Create Button**: Opens create form
- **✏️ Update Button**: Opens update form for selected booking
- **🗑️ Delete Button**: Deletes selected booking
- **❌ Cancel Button**: Cancels check-in for selected booking

### CreateFlightPassengerForm
- **✈️ Flight Selection**: Dropdown with active flights
- **👤 Passenger Selection**: Dropdown with all passengers
- **📝 Booking Reference**: Unique identifier input
- **🍽️ Special Requests**: Multi-line text for special needs
- **🎒 Baggage Info**: Multi-line text for baggage details

### UpdateFlightPassengerForm
- **📊 Read-only Info**: Flight, passenger, booking reference, check-in status
- **✏️ Editable Fields**: Special requests and baggage info
- **💾 Update Button**: Saves changes

## 🔄 Integration Points

### CheckinApp Integration
- **OptionsForm**: Entry point with "Flight Passenger Management" option
- **MainForm**: Updated to show options after login
- **Existing Check-in Flow**: Unchanged, still works with existing FlightPassenger records

### Server Integration
- **Dependency Injection**: FlightPassengerService registered in Program.cs
- **Existing Controllers**: Enhanced with new endpoints
- **Database**: Uses existing FlightPassenger model and relationships

### API Integration
- **RESTful Design**: Standard HTTP methods and status codes
- **Error Handling**: Comprehensive error responses
- **Logging**: All operations logged for audit trail

## 🧪 Testing Scenarios

### Create Scenarios
1. **✅ Valid Creation**: Flight exists, passenger exists, unique booking reference
2. **❌ Invalid Flight**: Flight doesn't exist
3. **❌ Invalid Passenger**: Passenger doesn't exist
4. **❌ Duplicate Booking**: Passenger already booked for flight
5. **❌ Duplicate Reference**: Booking reference already used

### Read Scenarios
1. **✅ Get All**: Returns all bookings
2. **✅ Get by ID**: Returns specific booking
3. **✅ Get by Flight**: Returns all passengers for flight
4. **✅ Get by Passenger**: Returns all flights for passenger
5. **✅ Get by Reference**: Returns booking by reference
6. **❌ Not Found**: Returns 404 for non-existent records

### Update Scenarios
1. **✅ Valid Update**: Updates special requests and baggage info
2. **❌ Invalid ID**: Booking doesn't exist
3. **✅ Empty Fields**: Handles null/empty values correctly

### Delete Scenarios
1. **✅ Valid Delete**: Deletes unchecked-in booking
2. **❌ Checked-in Delete**: Prevents deletion of checked-in bookings
3. **✅ Cascade Delete**: Removes related seat assignments and boarding passes

### Cancel Scenarios
1. **✅ Cancel Check-in**: Cancels check-in, releases resources
2. **✅ Cancel Non-checked**: Handles already non-checked bookings
3. **✅ Resource Release**: Releases seats and boarding passes

## 🚀 Usage Examples

### Creating a New Booking
```csharp
var createDto = new CreateFlightPassengerDto
{
    FlightId = 123,
    PassengerId = 456,
    BookingReference = "ABC123456",
    SpecialRequests = "Vegetarian meal",
    BaggageInfo = "1 checked bag",
    CreatedByEmployeeId = 789
};

var result = await _flightPassengerService.CreateFlightPassengerAsync(createDto);
```

### Updating a Booking
```csharp
var updateDto = new UpdateFlightPassengerDto
{
    SpecialRequests = "Updated special requests",
    BaggageInfo = "Updated baggage info",
    UpdatedByEmployeeId = 789
};

var result = await _flightPassengerService.UpdateFlightPassengerAsync(1001, updateDto);
```

### Getting Bookings by Flight
```csharp
var bookings = await _flightPassengerService.GetFlightPassengersByFlightAsync(123);
foreach (var booking in bookings)
{
    Console.WriteLine($"Booking: {booking.BookingReference}, Passenger: {booking.Passenger.FirstName}");
}
```

## 📊 Benefits

### 1. **Complete CRUD Control**
- ✅ Create new flight bookings
- ✅ Read and search bookings
- ✅ Update booking details
- ✅ Delete or cancel bookings

### 2. **Data Integrity**
- ✅ Comprehensive validation
- ✅ Prevents duplicate bookings
- ✅ Maintains referential integrity
- ✅ Audit trail for all operations

### 3. **User Experience**
- ✅ Intuitive UI with clear actions
- ✅ Real-time validation feedback
- ✅ Comprehensive error handling
- ✅ Efficient search and filtering

### 4. **Business Logic**
- ✅ Enforces booking rules
- ✅ Prevents invalid operations
- ✅ Maintains data consistency
- ✅ Supports audit requirements

### 5. **Integration**
- ✅ Works with existing check-in flow
- ✅ Maintains backward compatibility
- ✅ Extends current functionality
- ✅ Follows established patterns

## 🔧 Technical Implementation

### Service Registration
```csharp
// In Program.cs
builder.Services.AddScoped<IFlightPassengerService, FlightPassengerService>();
```

### Controller Endpoints
- All endpoints follow RESTful conventions
- Comprehensive error handling and logging
- Proper HTTP status codes
- Detailed API documentation

### Database Operations
- Uses existing Entity Framework repositories
- Maintains transaction integrity
- Proper cascade handling
- Audit trail support

### UI Components
- Modern WinForms design
- Responsive layout
- Clear user feedback
- Intuitive navigation

This comprehensive CRUD implementation provides complete control over FlightPassenger bookings while maintaining data integrity and providing an excellent user experience.
