
Given(/^I POST a encoded json "(.*)" as "(.*)" side under id "(.*)"$/) do |encodedJson,side,id|
    @last_response = HTTParty.put('http://localhost:5000/v1/diff/'+id+'/'+side, 
    :body => encodedJson.to_json,
    :headers => { 'Content-Type' => 'application/json' } )
end

When(/^I GET the diffs from id "(.*)"$/) do |id|
    @last_response = HTTParty.get('http://localhost:5000/v1/diff/'+id)
end

Then (/^I expect the response to be JSON:$/) do |json|
    expect(JSON.parse(@last_response.body)).to eq(JSON.parse(json))
end

Then (/^I expect the message to be "(.*)"/) do |message|
    expect(@last_response.body).to eq(message)
end